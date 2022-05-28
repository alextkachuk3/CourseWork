from tkinter import *

from HotelDatabase import HotelDatabase
from HotelServiceRequests import HotelServiceRequests

from config import db_host, db_port, db_user, db_password, db_name


def btn_command(q):
    print(q)


if __name__ == '__main__':
    database = HotelDatabase(db_host, db_port, db_user, db_password, db_name)
    # service = HotelServiceRequests()
    # service.register()
    # service.login()
    # service.add_hotel("Meow", "Meowland", "Wonderstreet 12")

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
            Button(frame, text=i, bg='green', width=9, height=4, command=lambda btn_num=i: btn_command(
                btn_num
            ))
        )
        btn_list[i].grid(column=x, row=y)
        x = x + 1
        if x == 7:
            x = 1
            y = y + 1

    root.mainloop()
