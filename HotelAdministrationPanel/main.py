from HotelServiceRequests import HotelServiceRequests

if __name__ == '__main__':
    service = HotelServiceRequests()
    service.register()
    service.login()
    service.add_hotel("Meow", "Meowland", "Wonderstreet 12")


