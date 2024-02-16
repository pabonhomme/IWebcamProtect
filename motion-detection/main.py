import cv2
import time
import datetime
import imutils
import numpy as np

# Initialiser la capture vidéo
video_capture = cv2.VideoCapture(0) # value (0) selects the devices default camera

first_frame = None

while True:
    frame = video_capture.read()[1]  # gives 2 outputs retval,frame - [1] selects frame

    greyscale_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)  # make each frame greyscale wich is needed for threshold

    gaussian_frame = cv2.GaussianBlur(greyscale_frame, (21, 21), 0)

    blur_frame = cv2.blur(gaussian_frame, (5, 5))

    greyscale_image = blur_frame

    if first_frame is None:
        first_frame = greyscale_image
    else:
        pass

    frame = imutils.resize(frame, width=500)
    frame_delta = cv2.absdiff(first_frame, greyscale_image)

    thresh = cv2.threshold(frame_delta, 100, 255, cv2.THRESH_BINARY)[1]

    dilate_image = cv2.dilate(thresh, None, iterations=2)

    # Dilater l'image pour remplir les trous
    dilated = cv2.dilate(thresh, None, iterations=3)

    # Trouver les contours
    contours, _ = cv2.findContours(dilated.copy(), cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)[-2:]

    # Dessiner les rectangles pour chaque contour
    for c in contours:
        if cv2.contourArea(c) > 800:  # if contour area is less then 800 non-zero(not-black) pixels(white)
            (x, y, w, h) = cv2.boundingRect(c)  # x,y are the top left of the contour and w,h are the width and hieght

            cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
        else:
            pass

    # Afficher le résultat
    cv2.imshow('Security Feed', frame)

    # Arrêter le programme si on appuie sur 'q'
    if cv2.waitKey(40) == ord('q'):
        break

# Libérer la caméra et fermer toutes les fenêtres ouvertes
cv2.destroyAllWindows()
