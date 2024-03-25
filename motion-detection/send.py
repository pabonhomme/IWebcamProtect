import requests


def send_image(url, image_path):
    with open(image_path, 'rb') as f_image:
        image_name = image_path.split("/")[-1]
        files = {'image': (image_name, f_image)}
        response = requests.post(url, files=files)

        return response

