import requests
import base64
import json

def send_image(url, image_path, detected_time, entity_type, camera_id, camera_ref):
    with open(image_path, 'rb') as f_image:
        image_base64 = "data:image/jpeg;base64," + base64.b64encode(f_image.read()).decode('utf-8')

        data = {
            'DetectedTime': detected_time,
            'ImageBase64': image_base64,
            'EntityTypeId': entity_type,
            'CameraId': camera_id,
            'CameraReference': camera_ref
        }

        headers = {
            'Content-Type': 'application/json'
        }

        response = requests.post(url, data=json.dumps(data), headers=headers, verify=False)
        return response
