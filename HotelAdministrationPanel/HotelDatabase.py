import pymysql as pymysql


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
                                                     "id INT AUTO_INCREMENT PRIMARY KEY, " \
                                                     "country VARCHAR(50)," \
                                                     "street VARCHAR(50)," \
                                                     "hotel_service_id INT);"
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
                                                        "description VARCHAR(1000), " \
                                                        "hotel_id INT," \
                                                        "FOREIGN KEY (hotel_id) " \
                                                        "REFERENCES hotels(id) " \
                                                        "ON UPDATE CASCADE ON DELETE CASCADE);"
                    cursor.execute(create_metro_stations_table_query)
                    print("Hotel numbers table created successfully")
                except pymysql.err.OperationalError:
                    print('Hotel numbers table creating failed')
                finally:
                    self.connection.commit()
