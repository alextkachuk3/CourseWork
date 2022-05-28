import base64
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
        return reply.json()

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
        return reply.json()

    def add_booking_order(self, hotel_number_id, date):
        data = json.dumps({
            "hotelNumberId": hotel_number_id,
            "year": date.year,
            "month": date.month,
            "day": date.day
        })
        headers = {"Content-Type": "application/json",
                   "Authorization": "bearer %s" % self.token}
        reply = requests.put(url=URL + '/api/HotelNumber/add_booking_order', data=data, headers=headers, verify=verify)
        print(reply.json())
        return reply.json()

    def add_image(self, hotel_number_id, image):
        base64_img = base64.encodebytes(image).decode('utf-8')
        data = json.dumps({
            "hotelNumberId": hotel_number_id,
            "base64ImageData": base64_img
        })
        headers = {"Content-Type": "application/json",
                   "Authorization": "bearer %s" % self.token}
        reply = requests.post(url=URL + '/api/Image/add_image', data=data, headers=headers, verify=verify)
        print(reply)
        print(reply.json())
        return reply.json()
