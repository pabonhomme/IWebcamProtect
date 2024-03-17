import cv2
import pandas as pd
import time
import os

# Initialiser la capture vidéo
cap = cv2.VideoCapture(0)

_, frame1 = cap.read()
_, frame2 = cap.read()

mouvements = []
en_mouvement = False
start_time = None

# Créer le détecteur de personnes HOG
hog = cv2.HOGDescriptor()
hog.setSVMDetector(cv2.HOGDescriptor_getDefaultPeopleDetector())

capture_folder = "captures/images_person"
if not os.path.exists(capture_folder):
    os.makedirs(capture_folder)

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
        #(x, y, w, h) = cv2.boundingRect(contour)
        #cv2.rectangle(frame1, (x, y), (x+w, y+h), (0, 255, 0), 2)
        timestamp = time.strftime("%Y-%m-%d_%H-%M-%S")
        capture_filename = os.path.join(capture_folder, f"person_{timestamp}.jpg")
        cv2.imwrite(capture_filename, frame1)

    if movement_detected:
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

# Parcourir le dossier de captures
for filename in os.listdir(capture_folder):
    if filename.endswith(".jpg"):
        image_path = os.path.join(capture_folder, filename)
        image = cv2.imread(image_path)

        # Détecter les personnes dans l'image
        (persons, weights) = hog.detectMultiScale(image, winStride=(4, 4), padding=(8, 8), scale=1.05)

        for i, (x, y, w, h) in enumerate(persons):
            if weights[i] > 0.7:
                cv2.rectangle(image, (x, y), (x + w, y + h), (255, 0, 0), 2)
                cv2.putText(frame1, "Person", (x, y), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 2)

                # Enregistrer l'image avec les rectangles de détection
                save_folder = "captures/images_person/detected"
                if not os.path.exists(save_folder):
                    os.makedirs(save_folder)
                cv2.imwrite(os.path.join(save_folder, filename), image)


cv2.destroyAllWindows()

# Créer un DataFrame
df_mouvements = pd.DataFrame(mouvements)

# Afficher le DataFrame
print(df_mouvements)