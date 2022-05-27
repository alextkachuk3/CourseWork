from HotelDatabase import HotelDatabase
from HotelServiceRequests import HotelServiceRequests

from config import db_host, db_port, db_user, db_password, db_name

if __name__ == '__main__':
    database = HotelDatabase(db_host, db_port, db_user, db_password, db_name)
    service = HotelServiceRequests()
    service.register()
    service.login()
    service.add_hotel("Meow", "Meowland", "Wonderstreet 12")


