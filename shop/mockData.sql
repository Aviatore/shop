insert into addresses (street, city, country) values ('Rigney', 'Curumaní', 'Colombia');
insert into addresses (street, city, country) values ('Ohio', 'Panique', 'Philippines');
insert into addresses (street, city, country) values ('Riverside', 'Hoima', 'Uganda');
insert into addresses (street, city, country) values ('Waywood', 'Plasy', 'Czech Republic');
insert into addresses (street, city, country) values ('Goodland', 'København', 'Denmark');
insert into addresses (street, city, country) values ('Sycamore', 'New York City', 'United States');
insert into addresses (street, city, country) values ('Chive', 'Malonty', 'Czech Republic');
insert into addresses (street, city, country) values ('Anniversary', 'Dūkštas', 'Lithuania');
insert into addresses (street, city, country) values ('Bluestem', 'Höganäs', 'Sweden');
insert into addresses (street, city, country) values ('Carey', 'Sumoto', 'Japan');

insert into authors (first_name, last_name) values ('Marga', 'Tweedlie');
insert into authors (first_name, last_name) values ('Nissa', 'Gulberg');
insert into authors (first_name, last_name) values ('Fernande', 'Timson');
insert into authors (first_name, last_name) values ('Esma', 'Haylands');
insert into authors (first_name, last_name) values ('Cecily', 'Jindacek');
insert into authors (first_name, last_name) values ('Bria', 'Bridgland');
insert into authors (first_name, last_name) values ('Idalina', 'Bodley');
insert into authors (first_name, last_name) values ('Barbra', 'Silk');
insert into authors (first_name, last_name) values ('Cassandre', 'Lidstone');
insert into authors (first_name, last_name) values ('Stanwood', 'Harbinson');

insert into genres (genre_name) values ('Action and adventure');
insert into genres (genre_name) values ('Criminal');
insert into genres (genre_name) values ('Fantasy');
insert into genres (genre_name) values ('Historical');
insert into genres (genre_name) values ('Horror');
insert into genres (genre_name) values ('Romance');

insert into publishers (publisher_name) values ('China Information Technology, Inc.');
insert into publishers (publisher_name) values ('Neustar, Inc.');
insert into publishers (publisher_name) values ('Strongbridge Biopharma plc');
insert into publishers (publisher_name) values ('WPCS International Incorporated');
insert into publishers (publisher_name) values ('Neuralstem, Inc.');
insert into publishers (publisher_name) values ('CSX Corporation');
insert into publishers (publisher_name) values ('Fiserv, Inc.');
insert into publishers (publisher_name) values ('Wells Fargo & Company');
insert into publishers (publisher_name) values ('Loral Space and Communications, Inc.');
insert into publishers (publisher_name) values ('Altisource Residential Corporation');

insert into users (user_name, email, phone) values ('Belvia', 'bvedekhin0@qq.com', 324564345);
insert into users (user_name, email, phone) values ('Jacquenetta', 'jstinson1@ebay.co.uk', 345434564);
insert into users (user_name, email, phone) values ('Worden', 'wclist2@virginia.edu', 702125487);
insert into users (user_name, email, phone) values ('Jo', 'jlemon3@scientificamerican.com', 570038230);
insert into users (user_name, email, phone) values ('Roby', 'rlefriec4@ning.com', 256726780);
insert into users (user_name, email, phone) values ('Hamish', 'hcaze5@cnn.com', 802570345);
insert into users (user_name, email, phone) values ('Ignacius', 'ihachard6@cbc.ca', 921117912);
insert into users (user_name, email, phone) values ('Alfie', 'aovenden7@posterous.com', 791109167);
insert into users (user_name, email, phone) values ('Annnora', 'awaterson8@zimbio.com', 660825047);
insert into users (user_name, email, phone) values ('Clair', 'colongain9@npr.org', 875360961);

insert into orders (billing_address_id, shipping_address_id, user_id, payment) VALUES (3, 2, 2, 0)
insert into orders (billing_address_id, shipping_address_id, user_id, payment) VALUES (2, 3, 3, 1)
insert into orders (billing_address_id, shipping_address_id, user_id, payment) VALUES (3, 4, 5, 1)
insert into orders (billing_address_id, shipping_address_id, user_id, payment) VALUES (5, 6, 1, 0)
insert into orders (billing_address_id, shipping_address_id, user_id, payment) VALUES (4, 7, 2, 0)
insert into orders (billing_address_id, shipping_address_id, user_id, payment) VALUES (4, 4, 2, 1)
insert into orders (billing_address_id, shipping_address_id, user_id, payment) VALUES (8, 8, 9, 0)
insert into orders (billing_address_id, shipping_address_id, user_id, payment) VALUES (6, 2, 3, 0)

insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Lost Battalion, The', 1, 5, 54, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Chairman of the Board', 2, 4, 123, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Slumber Party ''57', 3, 3, 58, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Beekeeper, The (O melissokomos)', 4, 2, 87, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Run Silent Run Deep', 5, 1, 73, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Little Bit of Heaven, A', 5, 2, 86, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Railway Children, The', 4, 3, 97, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Ripe', 3, 3, 78, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Wallace & Gromit: A Close Shave', 2, 2, 432, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');
insert into books (title, genre_id, publisher_id, price, description, fig_url) values ('Talhotblond:', 1, 1, 23, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et fringilla purus. Cras porta vulputate eros, porta cursus nulla vulputate et. Duis rhoncus mauris eu laoreet vulputate. Morbi sit amet facilisis sapien. Nunc in porta massa. Sed vitae turpis sit amet mi tincidunt efficitur a accumsan ante. Vivamus iaculis nisi a imperdiet viverra. Aenean maximus sodales leo ut vestibulum. Cras condimentum rutrum lacus, viverra auctor urna vulputate in. Aenean venenatis dui orci, et dictum justo maximus nec. Curabitur fermentum mauris.', 'https://images-na.ssl-images-amazon.com/images/I/51zMGt7CABL.jpg');

insert into logs (order_id, msg, timestamp) values (4, 'Order data accepted', '20210118 10:34:09 AM');
insert into logs (order_id, msg, timestamp) values (4, 'Start payment', '20210118 10:36:09 AM');
insert into logs (order_id, msg, timestamp) values (4, 'Payment accepted', '20210118 10:37:09 AM');
insert into logs (order_id, msg, timestamp) values (4, 'Sent order confirmation', '20210118 10:38:09 AM');
insert into logs (order_id, msg, timestamp) values (2, 'Order data accepted', '20210119 11:34:09 AM');
insert into logs (order_id, msg, timestamp) values (2, 'Start payment', '20210119 11:35:09 AM');
insert into logs (order_id, msg, timestamp) values (7, 'Order data accepted', '20210121 12:34:09 AM');
insert into logs (order_id, msg, timestamp) values (7, 'Start payment', '20210121 12:38:09 AM');
insert into logs (order_id, msg, timestamp) values (7, 'Payment accepted', '20210121 12:43:09 AM');

insert into books_ordered (order_id, book_id) VALUES (2, 2);
insert into books_ordered (order_id, book_id) VALUES (2, 5);
insert into books_ordered (order_id, book_id) VALUES (2, 3);
insert into books_ordered (order_id, book_id) VALUES (4, 1);
insert into books_ordered (order_id, book_id) VALUES (4, 5);
insert into books_ordered (order_id, book_id) VALUES (4, 6);
insert into books_ordered (order_id, book_id) VALUES (4, 4);
insert into books_ordered (order_id, book_id) VALUES (5, 7);
insert into books_ordered (order_id, book_id) VALUES (6, 2);
insert into books_ordered (order_id, book_id) VALUES (6, 1);
insert into books_ordered (order_id, book_id) VALUES (6, 3);
insert into books_ordered (order_id, book_id) VALUES (7, 4);
insert into books_ordered (order_id, book_id) VALUES (9, 2);

insert into author_book (book_id, author_id) values (1, 1);
insert into author_book (book_id, author_id) values (1, 2);
insert into author_book (book_id, author_id) values (2, 7);
insert into author_book (book_id, author_id) values (3, 1);
insert into author_book (book_id, author_id) values (4, 2);
insert into author_book (book_id, author_id) values (5, 3);
insert into author_book (book_id, author_id) values (6, 4);
insert into author_book (book_id, author_id) values (7, 9);
insert into author_book (book_id, author_id) values (8, 6);
insert into author_book (book_id, author_id) values (10, 4);
insert into author_book (book_id, author_id) values (10, 5);

