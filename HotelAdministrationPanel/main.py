from tkinter import *
from datetime import datetime, timedelta

from HotelDatabase import HotelDatabase
from HotelServiceRequests import HotelServiceRequests

from config import db_host, db_port, db_user, db_password, db_name


def btn_command(q):
    print(q)
    hotel_number_window = Toplevel(root)

    hotel_number_window['bg'] = '#fafafa'
    hotel_number_window.title('Hotel number ' + str(q) + ' booking')
    hotel_number_window.geometry('510x284')
    hotel_number_window.resizable(width=False, height=False)

    current_datetime = datetime.now()

    calendar_x = 1
    calendar_y = 1

    calendar_buttons_list = []
    dates_list = []

    for j in range(28):

        dates_list.append(current_datetime + timedelta(days=j))

        calendar_buttons_list.append(
            Button(hotel_number_window,
                   text=str(dates_list[j].day) + '/' + str(dates_list[j].month) + '/' + str(dates_list[j].year),
                   bg='green', width=9, height=4)
        )
        calendar_buttons_list[j].grid(column=calendar_x, row=calendar_y)

        calendar_x = calendar_x + 1
        if calendar_x == 8:
            calendar_x = 1
            calendar_y = calendar_y + 1

    print('test')


if __name__ == '__main__':
    database = HotelDatabase(db_host, db_port, db_user, db_password, db_name)
    service = HotelServiceRequests()
    service.register()
    service.login()
    service.add_hotel("Meow", "Meowland", "Wonderstreet 12")
    service.add_hotel_number("Very interesting description.", "2")

    # database.generate_db()

    root = Tk()

    root['bg'] = '#fafafa'
    root.title('Hotel administration panel')
    root.geometry('450x370')

    root.resizable(width=False, height=False)

    frame = Frame(root, bg='red')
    frame.place(relx=0.02, rely=0.02, relwidth=0.96, relheight=0.96)

    x = 1
    y = 1

    btn_list = []

    for i in range(30):
        btn_list.append(
            Button(frame, text=i, bg='#fc8403', width=9, height=4, command=lambda btn_num=i: btn_command(
                btn_num
            ))
        )
        btn_list[i].grid(column=x, row=y)
        x = x + 1
        if x == 7:
            x = 1
            y = y + 1

    root.mainloop()
