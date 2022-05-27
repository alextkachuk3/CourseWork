import json

import requests

from config import login, password, URL

verify = False


class HotelServiceRequests:
    @staticmethod
    def register():
        data = json.dumps({
            "login": login,
            "password": password
        })
        headers = {"Content-Type": "application/json"}
        reply = requests.post(url=URL + '/api/Auth/register', data=data, headers=headers, verify=verify)
        print(reply.json())
