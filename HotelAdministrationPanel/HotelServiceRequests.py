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

    def login(self):
        data = json.dumps({
            "login": login,
            "password": password
        })
        headers = {"Content-Type": "application/json"}
        reply = requests.post(url=URL + '/api/Auth/login', data=data, headers=headers, verify=verify)
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
        return reply.json()

    def add_booking_order(self, hotel_number_id, year, month, day):
        data = json.dumps({
            "hotelNumberId": hotel_number_id,
            "year": year,
            "month": month,
            "day": day
        })
        headers = {"Content-Type": "application/json",
                   "Authorization": "bearer %s" % self.token}
        reply = requests.put(url=URL + '/api/HotelNumber/add_booking_order', data=data, headers=headers, verify=verify)
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
        return reply.json()

    def get_booking_orders(self):
        headers = {"Content-Type": "application/json",
                   "Authorization": "bearer %s" % self.token}
        reply = requests.get(url=URL + '/api/HotelNumber/get_booking_orders', headers=headers, verify=verify)
        return reply.json()
