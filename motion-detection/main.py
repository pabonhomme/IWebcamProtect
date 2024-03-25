import cv2
import pandas as pd
from datetime import datetime, timezone
import time
import os
from send import send_image

# Initialiser la capture vidéo
cap = cv2.VideoCapture(0)

_, frame1 = cap.read()
_, frame2 = cap.read()

mouvements = []
en_mouvement = False
start_time = None

# Détecteur de personnes HOG
hog = cv2.HOGDescriptor()
hog.setSVMDetector(cv2.HOGDescriptor_getDefaultPeopleDetector())

capture_folder = "captures/video_person"
if not os.path.exists(capture_folder):
    os.makedirs(capture_folder)

prev_frame_time = 0

# used to record the time at which we processed current frame
new_frame_time = 0

fps_list = []

last_sent_time = time.time()

while True:
    person_detected = False
    _, frame = cap.read()
    new_frame_time = time.time()
    if frame is None:
        break

    fps = 1 / (new_frame_time - prev_frame_time)
    prev_frame_time = new_frame_time
    fps_list.append(fps)

    diff = cv2.absdiff(frame1, frame2)
    gray = cv2.cvtColor(diff, cv2.COLOR_BGR2GRAY)
    blur = cv2.GaussianBlur(gray, (5, 5), 0)
    _, thresh = cv2.threshold(blur, 20, 255, cv2.THRESH_BINARY)
    dilated = cv2.dilate(thresh, None, iterations=3)
    contours, _ = cv2.findContours(dilated, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)

    movement_detected = False

    # Détection de personnes
    (persons, weights) = hog.detectMultiScale(frame1, winStride=(4, 4), padding=(8, 8), scale=1.05)

    for i, (x, y, w, h) in enumerate(persons):
        current_time = datetime.now(timezone.utc)
        timestamp = current_time.strftime("%Y-%m-%dT%H:%M:%S.%f")[:-3] + "Z"

        if weights[i] > 0.7:
            person_detected = True
            cv2.rectangle(frame1, (x, y), (x + w, y + h), (255, 0, 0), 2)
            cv2.putText(frame1, "Person", (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 2)
            capture_filename = os.path.join(capture_folder, f"person_{timestamp}.jpg")
            cv2.imwrite(capture_filename, frame1)
            if time.time() - last_sent_time >= 1:
                last_sent_time = time.time()
                response = send_image("http://192.168.64.1:44311/api/services/app/DetectionEvent/Create", capture_filename,
                                  timestamp, 1, 1, "xref")
                print(response)

    if not person_detected:
        for contour in contours:
            current_time = datetime.now(timezone.utc)
            timestamp = current_time.strftime("%Y-%m-%dT%H:%M:%S.%f")[:-3] + "Z"

            if cv2.contourArea(contour) < 10000:
                continue

            movement_detected = True
            (x, y, w, h) = cv2.boundingRect(contour)
            cv2.rectangle(frame1, (x, y), (x+w, y+h), (0, 255, 0), 2)

            capture_filename = os.path.join(capture_folder, f"movement_{timestamp}.jpg")
            cv2.imwrite(capture_filename, frame1)
            if time.time() - last_sent_time >= 5:
                last_sent_time = time.time()
                response = send_image("http://192.168.64.1:44311/api/services/app/DetectionEvent/Create", capture_filename,
                                      timestamp, 2, 1, "xref")
                print(response)

    if movement_detected and not len(persons) == 0:
        # Mouvement détecté et personne identifiée
        if not en_mouvement:
            start_time = time.time()
            en_mouvement = True

    elif movement_detected:
        # Mouvement détecté mais pas de personne
        if not en_mouvement:
            start_time = time.time()
            en_mouvement = True

            timestamp = time.strftime("%Y-%m-%d_%H-%M-%S")

    if not movement_detected and en_mouvement:
        end_time = time.time()
        mouvements.append({'start': start_time, 'end': end_time})
        en_mouvement = False

    cv2.putText(frame1, 'Haute confiance', (10, 15), cv2.FONT_HERSHEY_SIMPLEX, 0.7, (255, 0, 0), 2)

    cv2.imshow("feed", frame1)
    frame1 = frame2
    _, frame2 = cap.read()

    if cv2.waitKey(40) == ord('q'):
        break

# Libérer la caméra et fermer toutes les fenêtres ouvertes
cap.release()
cv2.destroyAllWindows()

# Créer un DataFrame
df_mouvements = pd.DataFrame(mouvements)

# Afficher le DataFrame
print(df_mouvements)

print(fps_list)
print("FPS Moyen", sum(fps_list)/len(fps_list))