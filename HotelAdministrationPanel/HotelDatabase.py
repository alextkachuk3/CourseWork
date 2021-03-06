import os

import pymysql as pymysql
import lorem


def convert_to_binary_data(filename):
    with open(filename, 'rb') as file:
        binary_data = file.read()
    return binary_data


class HotelDatabase:
    def __init__(self, db_host, db_port, db_user, db_password, db_name):
        self.connection = pymysql.connect(
            host=db_host,
            port=db_port,
            user=db_user,
            password=db_password,
            database=db_name
        )
        print('Successfully connected to MySQL database...')
        self.init_tables()

    def __del__(self):
        self.connection.close()
        print('Connection to MySQL database closed...')

    def get_images(self):
        with self.connection.cursor() as cursor:
            get_images_query = "SELECT * FROM images"
            cursor.execute(get_images_query)
            return cursor.fetchall()

    def get_hotels(self):
        with self.connection.cursor() as cursor:
            get_hotels_query = "SELECT * FROM hotels"
            cursor.execute(get_hotels_query)
            return cursor.fetchall()

    def get_hotel_numbers(self):
        with self.connection.cursor() as cursor:
            get_hotel_numbers_query = "SELECT * FROM hotel_numbers"
            cursor.execute(get_hotel_numbers_query)
            return cursor.fetchall()

    def get_booking_orders(self):
        with self.connection.cursor() as cursor:
            get_hotel_numbers_query = "SELECT * FROM booking_orders"
            cursor.execute(get_hotel_numbers_query)
            return cursor.fetchall()

    def get_hotel_number_booking_orders(self, hotel_number_id, date=None):
        with self.connection.cursor() as cursor:
            if date is None:
                get_hotel_numbers_query = "SELECT * FROM booking_orders WHERE hotel_number_id = %s"
                get_hotel_numbers_val = hotel_number_id
            else:
                get_hotel_numbers_query = "SELECT * FROM booking_orders " \
                                          "WHERE hotel_number_id = %s AND year = %s AND month = %s AND day = %s"
                get_hotel_numbers_val = (hotel_number_id, date.year, date.month.date.day)
            cursor.execute(get_hotel_numbers_query, get_hotel_numbers_val)
            return cursor.fetchall()

    def get_service_id_hotel_by_id(self, hotel_id):
        with self.connection.cursor() as cursor:
            get_hotel_by_id_query = "SELECT booking_service_id FROM hotels WHERE id = %s"
            cursor.execute(get_hotel_by_id_query, hotel_id)
            return cursor.fetchone()

    def get_service_id_hotel_number_by_id(self, hotel_number_id):
        with self.connection.cursor() as cursor:
            get_hotel_by_id_query = "SELECT booking_service_id FROM hotel_numbers WHERE id = %s"
            cursor.execute(get_hotel_by_id_query, hotel_number_id)
            return cursor.fetchone()

    def update_hotel_service_id(self, hotel_id, hotel_service_id):
        try:
            with self.connection.cursor() as cursor:
                update_hotel_service_id_query = "UPDATE hotels SET booking_service_id = %s WHERE id = %s"
                update_hotel_service_id_val = (hotel_service_id, hotel_id)
                cursor.execute(update_hotel_service_id_query, update_hotel_service_id_val)
        finally:
            self.connection.commit()

    def update_hotel_number_service_id(self, hotel_number_id, hotel_number_service_id):
        try:
            with self.connection.cursor() as cursor:
                update_hotel_number_service_id_query = "UPDATE hotel_numbers SET booking_service_id = %s WHERE id = %s"
                update_hotel_number_service_id_val = (hotel_number_service_id, hotel_number_id)
                cursor.execute(update_hotel_number_service_id_query, update_hotel_number_service_id_val)
        finally:
            self.connection.commit()

    def update_booking_order_service_id(self, booking_order_id, booking_order_service_id):
        try:
            with self.connection.cursor() as cursor:
                update_booking_order_service_id_query = "UPDATE booking_orders" \
                                                        " SET booking_service_id = %s" \
                                                        " WHERE id = %s"
                update_booking_order_service_id_val = (booking_order_service_id, booking_order_id)
                cursor.execute(update_booking_order_service_id_query, update_booking_order_service_id_val)
        finally:
            self.connection.commit()

    def update_image_service_id(self, image_id, image_service_id):
        try:
            with self.connection.cursor() as cursor:
                update_image_service_id_query = "UPDATE images SET booking_service_id = %s WHERE id = %s"
                update_image_service_id_val = (image_service_id, image_id)
                cursor.execute(update_image_service_id_query, update_image_service_id_val)
        finally:
            self.connection.commit()

    def add_booking_order(self, hotel_number_id, first_name, last_name, date):
        try:
            with self.connection.cursor() as cursor:
                add_booking_order_query = \
                    "INSERT INTO booking_orders (first_name, last_name, year, month, day, hotel_number_id)" \
                    " VALUES (%s, %s, %s, %s, %s, %s) "
                add_booking_order_val = (first_name, last_name, date.year, date.month, date.day, hotel_number_id)
                cursor.execute(add_booking_order_query, add_booking_order_val)
        finally:
            self.connection.commit()

    def add_booking_order_from_service(self, hotel_number_id, first_name, last_name, year, month, day, service_id):
        try:
            with self.connection.cursor() as cursor:
                add_booking_order_query = \
                    "INSERT INTO booking_orders (first_name, last_name, year, month, day, hotel_number_id, " \
                    "booking_service_id)" \
                    " VALUES (%s, %s, %s, %s, %s, %s, %s) "
                add_booking_order_val = (first_name, last_name, year, month, day, hotel_number_id, service_id)
                cursor.execute(add_booking_order_query, add_booking_order_val)
        finally:
            self.connection.commit()

    def generate_db(self):
        with self.connection.cursor() as cursor:
            try:
                cursor.execute("DROP TABLE IF EXISTS images")
                cursor.execute("DROP TABLE IF EXISTS booking_orders")
                cursor.execute("DROP TABLE IF EXISTS hotel_numbers")
                cursor.execute("DROP TABLE IF EXISTS hotels")
            finally:
                self.connection.commit()

        self.init_tables()

        with self.connection.cursor() as cursor:
            try:
                insert_hotel_query = "INSERT INTO hotels (name, city, address) VALUES(%s, %s, %s)"
                insert_hotel_val1 = ["Oskar", "Warsaw", "Unknown street 7"]
                insert_hotel_val2 = ["Atlantic", "Venice", "Unknown street 12"]
                cursor.execute(insert_hotel_query, insert_hotel_val1)
                cursor.execute(insert_hotel_query, insert_hotel_val2)

                insert_hotel_number_query = "INSERT INTO hotel_numbers (description, hotel_id) VALUES(%s, %s)"

                for i in range(30):
                    cursor.execute(insert_hotel_number_query, [lorem.text()[:400], 1])

                for i in range(10):
                    cursor.execute(insert_hotel_number_query, [lorem.text()[:400], 2])

                image_counter = 1

                insert_image_query = "INSERT INTO images (image_blob, hotel_number_id) VALUES(%s, %s)"

                for i in range(30):
                    for j in range(5):
                        cursor.execute(insert_image_query,
                                       [convert_to_binary_data(os.path.dirname(os.path.abspath(__file__)) +
                                                               '\\hotel_photos\\'
                                                               + str(image_counter) +
                                                               '.jpg'), i + 1])

                        image_counter = image_counter + 1

                        # 25 count of images in folder
                        if image_counter == 25:
                            image_counter = 1

                for i in range(10):
                    for j in range(5):
                        cursor.execute(insert_image_query,
                                       [convert_to_binary_data(os.path.dirname(os.path.abspath(__file__)) +
                                                               '\\hotel_photos\\'
                                                               + str(image_counter) +
                                                               '.jpg'), i + 31])

                        image_counter = image_counter + 1

                        # 25 count of images in folder
                        if image_counter == 25:
                            image_counter = 1

            finally:
                self.connection.commit()

    def get_tables_list(self):
        table_name_list = []
        with self.connection.cursor() as cursor:
            cursor.execute('SHOW TABLES')
            for x in cursor:
                table_name_list.append(x[0])
        return table_name_list

    def init_tables(self):
        with self.connection.cursor() as cursor:
            table_list = self.get_tables_list()

            if 'hotels' not in table_list:
                try:
                    create_metro_lines_table_query = "CREATE TABLE hotels(" \
                                                     "id INT AUTO_INCREMENT PRIMARY KEY," \
                                                     "name VARCHAR(50)," \
                                                     "city VARCHAR(50)," \
                                                     "address VARCHAR(50)," \
                                                     "booking_service_id INT);"
                    cursor.execute(create_metro_lines_table_query)
                    print("Hotels table created successfully")
                except pymysql.err.OperationalError:
                    print('Hotels table creating failed')
                finally:
                    self.connection.commit()

            if 'hotel_numbers' not in table_list:
                try:
                    create_metro_stations_table_query = "CREATE TABLE hotel_numbers(" \
                                                        "id INT AUTO_INCREMENT PRIMARY KEY, " \
                                                        "description VARCHAR(401), " \
                                                        "hotel_id INT," \
                                                        "booking_service_id INT," \
                                                        "FOREIGN KEY (hotel_id) " \
                                                        "REFERENCES hotels(id) " \
                                                        "ON UPDATE CASCADE ON DELETE CASCADE);"
                    cursor.execute(create_metro_stations_table_query)
                    print("Hotel numbers table created successfully")
                except pymysql.err.OperationalError:
                    print('Hotel numbers table creating failed')
                finally:
                    self.connection.commit()

            if 'images' not in table_list:
                try:
                    create_metro_stations_table_query = "CREATE TABLE images(" \
                                                        "id INT AUTO_INCREMENT PRIMARY KEY, " \
                                                        "image_blob LONGBLOB," \
                                                        "hotel_number_id INT," \
                                                        "booking_service_id INT," \
                                                        "FOREIGN KEY (hotel_number_id) " \
                                                        "REFERENCES hotel_numbers(id) " \
                                                        "ON UPDATE CASCADE ON DELETE CASCADE);"
                    cursor.execute(create_metro_stations_table_query)
                    print("Images table created successfully")
                except pymysql.err.OperationalError:
                    print('Images table creating failed')
                finally:
                    self.connection.commit()

            if 'booking_orders' not in table_list:
                try:
                    create_metro_stations_table_query = "CREATE TABLE booking_orders(" \
                                                        "id INT AUTO_INCREMENT PRIMARY KEY, " \
                                                        "first_name VARCHAR(100)," \
                                                        "last_name VARCHAR(100)," \
                                                        "year INT," \
                                                        "month INT," \
                                                        "day INT," \
                                                        "hotel_number_id INT," \
                                                        "booking_service_id INT," \
                                                        "FOREIGN KEY (hotel_number_id) " \
                                                        "REFERENCES hotel_numbers(id) " \
                                                        "ON UPDATE CASCADE ON DELETE CASCADE);"
                    cursor.execute(create_metro_stations_table_query)
                    print("Booking orders table created successfully")
                except pymysql.err.OperationalError:
                    print('Booking orders table creating failed')
                finally:
                    self.connection.commit()
