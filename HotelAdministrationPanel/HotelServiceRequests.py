import json

import requests

from config import login, password, URL

verify = False


class HotelServiceRequests:
    def __init__(self):
        self.token = None

    @staticmethod
    def register():
        data = json.dumps({
            "login": login,
            "password": password
        })
        headers = {"Content-Type": "application/json"}
        reply = requests.post(url=URL + '/api/Auth/register', data=data, headers=headers, verify=verify)
        print(reply.json())

    def login(self):
        data = json.dumps({
            "login": login,
            "password": password
        })
        headers = {"Content-Type": "application/json"}
        reply = requests.get(url=URL + '/api/Auth/login', data=data, headers=headers, verify=verify)
        print(reply.text)
        self.token = reply.text
