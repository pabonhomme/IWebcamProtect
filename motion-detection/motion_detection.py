import cv2
import pandas as pd
import time
import os
import requests
from send import send_image
from datetime import datetime, timezone


# Initialiser la capture vidéo
cap = cv2.VideoCapture(0)

_, frame1 = cap.read()
_, frame2 = cap.read()

mouvements = []
en_mouvement = False
start_time = None

capture_folder = "captures/motion"
if not os.path.exists(capture_folder):
    os.makedirs(capture_folder)

capture_folder_faces = "captures/motion/faces"
if not os.path.exists(capture_folder):
    os.makedirs(capture_folder)

last_sent_time = time.time()

while True:
    _, frame = cap.read()
    if frame is None:
        break

    diff = cv2.absdiff(frame1, frame2)
    gray = cv2.cvtColor(diff, cv2.COLOR_BGR2GRAY)
    blur = cv2.GaussianBlur(gray, (5, 5), 0)
    _, thresh = cv2.threshold(blur, 20, 255, cv2.THRESH_BINARY)
    dilated = cv2.dilate(thresh, None, iterations=3)
    contours, _ = cv2.findContours(dilated, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)

    movement_detected = False
    for contour in contours:
        if cv2.contourArea(contour) < 15000:
            continue

        movement_detected = True
        (x, y, w, h) = cv2.boundingRect(contour)
        cv2.rectangle(frame1, (x, y), (x + w, y + h), (0, 255, 0), 2)
        current_time = datetime.now(timezone.utc)
        timestamp = current_time.strftime("%Y-%m-%dT%H:%M:%S.%f")[:-3] + "Z"
        capture_filename = os.path.join(capture_folder, f"person_{timestamp}.jpg")
        cv2.imwrite(capture_filename, frame1)
        if time.time() - last_sent_time >= 5:
            last_sent_time = time.time()
            response = send_image("http://192.168.64.1:44311/api/services/app/DetectionEvent/Create", capture_filename,
                                  str(timestamp), 1, 1, "xref")
            print(response.text)


    if movement_detected :
        # Mouvement détecté
        if not en_mouvement:
            start_time = time.time()
            en_mouvement = True

    if not movement_detected and en_mouvement:
        end_time = time.time()
        mouvements.append({'start': start_time, 'end': end_time})
        en_mouvement = False

    cv2.imshow("Security feed", frame1)
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


def send_image(url, image_path):
    # Ouvrir l'image en mode binaire
    with open(image_path, 'rb') as f_image:
        nom_image = image_path.split("/")[-1]
        files = {'image': (nom_image, f_image)}

        # Envoyer la requête POST avec l'image
        response = requests.post(url, files=files)

        return response
