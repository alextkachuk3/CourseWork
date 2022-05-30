from HotelDatabase import HotelDatabase
from config import db_host, db_port, db_user, db_password, db_name

database = HotelDatabase(db_host, db_port, db_user, db_password, db_name)
database.generate_db()
