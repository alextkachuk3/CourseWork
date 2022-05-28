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
        reply = requests.post(url=URL + '/api/Auth/login', data=data, headers=headers, verify=verify)
        print(reply.text)
        self.token = reply.text

    def add_hotel(self, name, city, address):
        data = json.dumps({
            "name": name,
            "city": city,
            "address": address
        })
        headers = {"Content-Type": "application/json",
                   "Authorization": "bearer %s" % self.token}
        reply = requests.post(url=URL + '/api/Hotel/add_hotel', data=data, headers=headers, verify=verify)
        print(reply.json())

    def add_hotel_number(self, description, hotel_id):
        data = json.dumps({
            "hotelId": hotel_id,
            "price": 1000,
            "description": description
        })
        headers = {"Content-Type": "application/json",
                   "Authorization": "bearer %s" % self.token}
        reply = requests.post(url=URL + '/api/HotelNumber/add_hotel_number', data=data, headers=headers, verify=verify)
        print(reply.json())


