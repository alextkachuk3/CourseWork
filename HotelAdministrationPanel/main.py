from tkinter import *
from datetime import datetime, timedelta

from HotelDatabase import HotelDatabase
from HotelServiceRequests import HotelServiceRequests

from config import db_host, db_port, db_user, db_password, db_name

database = HotelDatabase(db_host, db_port, db_user, db_password, db_name)
service = HotelServiceRequests()


def upload_all_hotel_data():
    hotels = database.get_hotels()
    for hotel in hotels:
        if hotel[4] is None:
            reply = service.add_hotel(hotel[1], hotel[2], hotel[3])
            database.update_hotel_service_id(hotel[0], reply['id'])

    hotel_numbers = database.get_hotel_numbers()
    for hotel_number in hotel_numbers:
        if hotel_number[3] is None:
            service_id = database.get_service_id_hotel_by_id(hotel_number[2])[0]
            reply = service.add_hotel_number(hotel_number[1], service_id)
            database.update_hotel_number_service_id(hotel_number[0], reply['id'])

    images = database.get_images()
    for image in images:
        if image[3] is None:
            service_id = database.get_service_id_hotel_number_by_id(image[2])[0]
            reply = service.add_image(service_id, image[1])
            database.update_image_service_id(image[0], reply['id'])

    booking_orders = database.get_booking_orders()
    for booking_order in booking_orders:
        if booking_order[7] is None:
            service_id = database.get_service_id_hotel_number_by_id(booking_order[6])[0]
            reply = service.add_booking_order(service_id, booking_order[3], booking_order[4], booking_order[5])
            database.update_booking_order_service_id(booking_order[0], reply['id'])


def update_booking_status():
    service_booking_orders = service.get_booking_orders()
    local_booking_orders = database.get_booking_orders()
    for s in range(len(service_booking_orders)):
        found = False
        for l in range(len(local_booking_orders)):
            if service_booking_orders[s]['id'] == local_booking_orders[l][7]:
                found = True
                break
        if found is False:
            database.add_booking_order_from_service(service_booking_orders[s]['hotelNumberId'],
                                                    service_booking_orders[s]['firstName'],
                                                    service_booking_orders[s]['lastName'],
                                                    service_booking_orders[s]['year'],
                                                    service_booking_orders[s]['month'],
                                                    service_booking_orders[s]['day'],
                                                    service_booking_orders[s]['id'])


def process_booking(hotel_number_id, date, first_name, last_name, booking_confirm_window, hotel_number_window):
    hotel_number_window.deiconify()
    booking_confirm_window.destroy()
    database.add_booking_order(hotel_number_id, first_name, last_name, date)
    upload_all_hotel_data()


def on_date_panel_closing(window):
    root.deiconify()
    window.destroy()


def on_process_booking_closing(booking_window, hotel_number_window):
    hotel_number_window.deiconify()
    booking_window.destroy()


def btn_order(hotel_number_id, date, hotel_number_window):
    hotel_number_window.withdraw()
    booking_window = Toplevel(hotel_number_window)
    booking_window.geometry('200x250')
    booking_window.resizable(0, 0)
    booking_window.protocol("WM_DELETE_WINDOW",
                            lambda w1=booking_window, w2=hotel_number_window: on_process_booking_closing(w1, w2))
    _hotel_number_label = Label(booking_window, text='Hotel number')
    _hotel_number_label.grid(row=1, column=1)
    hotel_number_label = Label(booking_window, text=str(hotel_number_id))
    hotel_number_label.grid(row=2, column=1)
    _date_label = Label(booking_window, text='Date (DD/MM/YY)')
    _date_label.grid(row=3, column=1)
    date_label = Label(booking_window, text=str(date.day) + '/' + str(date.month) + '/' + str(date.year))
    date_label.grid(row=4, column=1)
    first_name_label = Label(booking_window, text='First name')
    first_name_label.grid(row=5, column=1)
    first_name_input = Entry(booking_window)
    first_name_input.grid(row=6, column=1)
    last_name_label = Label(booking_window, text='Last name')
    last_name_label.grid(row=7, column=1)
    last_name_input = Entry(booking_window)
    last_name_input.grid(row=8, column=1)
    confirm_button = Button(booking_window, text='Confirm',
                            command=lambda b_w=booking_window, h_n_w=hotel_number_window: process_booking(
                                hotel_number_id, date, first_name_input.get(), last_name_input.get(), b_w, h_n_w))
    confirm_button.grid(row=9, column=1)


def on_date_panel_closing(window):
    root.deiconify()
    window.destroy()


def order_info_on_close(booking_order_window, hotel_number_window):
    hotel_number_window.deiconify()
    booking_order_window.destroy()


def order_info(booking_order, hotel_number_window):
    hotel_number_window.withdraw()
    booking_order_window = Toplevel(hotel_number_window)
    booking_order_window.resizable(0, 0)
    booking_order_window.protocol("WM_DELETE_WINDOW",
                                  lambda w1=booking_order_window, w2=hotel_number_window: order_info_on_close(w1, w2))
    booking_order_window.geometry('200x250')
    _booking_order_id = Label(booking_order_window, text="Booking order ID")
    _booking_order_id.grid(row=1, column=1)
    booking_order_id = Label(booking_order_window, text=booking_order[7])
    booking_order_id.grid(row=2, column=1)
    _label_first_name = Label(booking_order_window, text="First name")
    _label_first_name.grid(row=3, column=1)
    label_first_name = Label(booking_order_window, text=booking_order[1])
    label_first_name.grid(row=4, column=1)
    _label_last_name = Label(booking_order_window, text="Last name")
    _label_last_name.grid(row=5, column=1)
    label_last_name = Label(booking_order_window, text=booking_order[2])
    label_last_name.grid(row=6, column=1)


def btn_command(hotel_number_id):
    update_booking_status()
    root.withdraw()
    hotel_number_window = Toplevel(root)
    hotel_number_window.protocol("WM_DELETE_WINDOW", lambda window=hotel_number_window: on_date_panel_closing(window))
    hotel_number_window['bg'] = '#fafafa'
    hotel_number_window.title('Hotel number ' + str(hotel_number_id) + ' booking')
    hotel_number_window.geometry('510x284')
    hotel_number_window.resizable(width=False, height=False)

    current_datetime = datetime.now()

    calendar_x = 1
    calendar_y = 1

    calendar_buttons_list = []
    dates_list = []

    booking_orders = database.get_booking_orders()

    current_hotel_number_booking_orders = []

    for booking_order in booking_orders:
        if booking_order[6] == hotel_number_id:
            current_hotel_number_booking_orders.append(booking_order)

    for j in range(28):

        dates_list.append(current_datetime + timedelta(days=j))

        booking_order_exist_for_current_day = False
        booking_order_for_current_day = None

        for booking_order in current_hotel_number_booking_orders:
            if (dates_list[j].year == booking_order[3]
                    and dates_list[j].month == booking_order[4]
                    and dates_list[j].day == booking_order[5]):
                booking_order_exist_for_current_day = True
                booking_order_for_current_day = booking_order
                break

        if booking_order_exist_for_current_day:
            calendar_buttons_list.append(
                Button(hotel_number_window,
                       text=str(dates_list[j].day) + '/' + str(dates_list[j].month) + '/' + str(dates_list[j].year),
                       bg='yellow', width=9, height=4,
                       command=lambda b_o=booking_order_for_current_day, h_n_w=hotel_number_window: order_info(b_o,
                                                                                                               h_n_w))
            )
        else:
            calendar_buttons_list.append(
                Button(hotel_number_window,
                       text=str(dates_list[j].day) + '/' + str(dates_list[j].month) + '/' + str(dates_list[j].year),
                       bg='green', width=9, height=4,
                       command=lambda hotel_n=hotel_number_id, selected_date=dates_list[j]: btn_order(hotel_n,
                                                                                                      selected_date,
                                                                                                      hotel_number_window))
            )
        calendar_buttons_list[j].grid(column=calendar_x, row=calendar_y)

        calendar_x = calendar_x + 1
        if calendar_x == 8:
            calendar_x = 1
            calendar_y = calendar_y + 1


if __name__ == '__main__':

    service.register()
    service.login()

    upload_all_hotel_data()

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
