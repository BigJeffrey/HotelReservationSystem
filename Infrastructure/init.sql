create table customer (
customer_id SERIAL primary KEY,
first_name VARCHAR(50) not null,
last_name VARCHAR(50) not null,
email varchar(100) not null,
phone_number varchar(20),
created_at timestamp default now()
);

alter table customer add constraint uq_customers_email unique (email);
alter table customer add constraint uq_customers_phone_number unique (phone_number);

create type room_type_enum AS ENUM ('basic', 'business', 'deluxe');

create table room (
room_id serial primary key,
room_number varchar(10) not null,
room_type text not null,
price_per_night decimal(10,2) not null,
capacity int not null,
is_available boolean default true
);

alter table room add constraint uq_rooms_room_number unique (room_number);

create table booking (
booking_id serial primary key,
customer_id int not null,
booking_date timestamp default now(),
start_date date not null,
end_date date not null,
status varchar(20) default 'pending'
);

alter table booking add constraint fk_booking_customer foreign key(customer_id) references customer(customer_id) on delete cascade;

-- many to many
create table booking_detail (
booking_detail_id serial primary key,
booking_id int not null,
room_id int not null,
price decimal(10,2) not null,
nights int not null
);

alter table booking_detail add constraint fk_booking_details_booking foreign key(booking_id) references booking(booking_id) on delete cascade;
alter table booking_detail add constraint fk_booking_details_room foreign key(room_id) references room(room_id) on delete cascade;

create table payment (
payment_id serial primary key,
booking_id int not null,
amount decimal(10,2) not null,
payment_date timestamp default now(),
payment_method varchar(50),
status varchar(20) default 'pending'
);

alter table payment add constraint fk_payments_booking FOREIGN key(booking_id) references booking(booking_id) on delete cascade;

create table extra_service (
extra_service_id serial primary key,
name varchar(100) not null,
description TEXT,
price decimal(10,2) not null
);

alter table extra_service add constraint uq_extra_services_name unique (name);

-- many to many
create table booking_service (
booking_services_id serial primary key,
booking_id int not null,
extra_service_id int not null,
quantity int default 1,
total_price decimal(10,2) not null
);

alter table booking_service add constraint fk_booking_services_booking foreign key(booking_id) references booking(booking_id) on delete cascade;
alter table booking_service add constraint fk_booking_services_extra_service foreign key(extra_service_id) references extra_service(extra_service_id) on delete cascade;