
-- 1) Authors,
-- 2) BookAuthors,
-- 3) Reader,
-- 4) Book,
-- 5) BookRepository,
-- 6) IssuingJournal,
-- 7) Roles,
-- 8) ReaderRoles.

-----------------------------------------------------------------------------------------------------------------------------------------------------------------

insert into Author(FirstName, LastName, NickName, BirthDate, PhotoUrl)
values
('Robert Lewis', 'Stevenson', 'Lui',      '1850-11-13', 'http://www.google.com'),
('Jonathan',     'Swift',     'Johny',    '1667-11-30', 'http://www.google.com'),
('Andrew',       'Nekrasov',  'Dron',     '1907-06-22', 'http://www.google.com'),
('Nickolay',     'Nosov',     'NickNick', '1908-11-23', 'http://www.google.com'),
('James Paul', 'Czajkowski', 'James Rollins', '1961-08-20', 'https://ru.wikipedia.org/wiki/%D0%94%D0%B6%D0%B5%D0%B9%D0%BC%D1%81_%D0%A0%D0%BE%D0%BB%D0%BB%D0%B8%D0%BD%D1%81#/media/File:Author_james_rollins_2008.jpg'),
('Stephen Edwin', 'King', null, '1947-09-21', 'https://ru.wikipedia.org/wiki/%D0%9A%D0%B8%D0%BD%D0%B3,_%D0%A1%D1%82%D0%B8%D0%B2%D0%B5%D0%BD#/media/File:Stephen_King,_Comicon.jpg'),
('Aleksandr', 'Chubarian', 'Sanych', '1974-11-19', 'http://www.etnogenez.ru/img/wiki/front/b/793.jpg?1303745822'),
('Dan', 'Brown', null, '1964-06-22', 'https://ru.wikipedia.org/wiki/%D0%91%D1%80%D0%B0%D1%83%D0%BD,_%D0%94%D1%8D%D0%BD#/media/File:Dan_Brown_bookjacket_cropped.jpg'),
('Ray Douglas', 'Bradbury', null, '1920-08-20', 'https://ru.wikipedia.org/wiki/%D0%91%D1%80%D1%8D%D0%B4%D0%B1%D0%B5%D1%80%D0%B8,_%D0%A0%D1%8D%D0%B9#/media/File:Ray_Bradbury_(1975).jpg')
;

--select * from Author

-----------------------------------------------------------------------------------------------------------------------------------------------------------------
insert into BookAuthors(BookID, AuthorID)
values
(1, 1),(2, 2),(3, 3),(4, 4),
(5, 7),(6, 7),(7, 7),(8, 8),
(9, 8),(10, 8),(11, 8),(12, 5),
(13, 5),(14, 5),(15, 6),(16, 6),
(16, 9),(17, 6),(18, 6),(19, 6);

--select * from BookAuthors

-----------------------------------------------------------------------------------------------------------------------------------------------------------------

insert into Reader(FirstName, LastName, NickName, Adress, Email, Phone, PhotoUrl, [Password])
values
('Kravchenko', 'Darja', 'Dinka', 'Dnepr, Ukraine', 'dinnnka@gmail.com', '+380935545443', 'www.vk.com', 'dp150693kds'),
('Fediukovich', 'Dmitriy', 'CorpArmstrong', 'Toronto, Canada', 'cherry.cake128@gmail.com', '+380933889153', 'www.facebook.com', 'BioMan'),
('Hetfield', 'James', 'PapaHet', 'San Francisco, USA', 'metallica@gmail.com', '+198542363654', 'www.metallica.com', 'metall'), 
('Alisa','Milano','milly','San Francisco, USA','alisa.milano@gmail.com','+125469873256',null,'alis123'),
('Sheldon','Cuper','shelder','Texas, USA','Sheldon.Cuper@gmail.com','+125478963254',null,'shelder21'),
('Lubov','Grebenuk','grannyLuba','Dnepr, Ukraine','Lubov.Grebenuk@gmail.com','+380952214785',null,'kuzia10'),
('Valentina','Parfenova','ValyaGranny','Ekaterinburg, Russia','Valentina.Parfenova@gmail.com','+798541366541',null,'katashka61');

--select * from Reader

-----------------------------------------------------------------------------------------------------------------------------------------------------------------

insert into Book(Name, PublishDate, Genre, ImageUrl)
values
('Treasures Island',							'1979-06-29',	'Novel',		'http://diletant.media/upload/iblock/76d/76d19654cbb9b9c67fd737e1be78a712.jpg'),
('The adventures of Gulliver',	    			'1924-07-15',	'Adventure',	'https://audioknigi.club/uploads/topics/preview/00/00/05/68/8d33708b1e.jpg'),
('The adventures of Captain Vrungel',			'1981-10-05',	'Adventure',	'http://i.ucrazy.ru/files/pics/2016.02/1455008983_1.jpg'),
('The adventures of Neznaika and his friends',	'1965-12-03',	'Fairy Tale',	'http://www.alinino.az/upload/iblock/250/250602cf3aca8f029e179339bd1bf26b.jpg'),
('Full Root', '2006-04-25', 'KiberPunk', 'https://upload.wikimedia.org/wikipedia/ru/6/69/Sanych_root.jpg'),
('Non-return Point', '2008-06-13', 'KiberPunk', 'http://www.e-reading.club/cover/125/125953.jpg'),
('One million for idiots', '2010-08-14', 'KiberPunk', 'http://fb2.kz/uploads/posts/2016-01/1452252843_hakery-million-dlya-idiotov.jpg'),
('Inferno', '2013-07-28', 'Novel', 'http://vignette2.wikia.nocookie.net/davincicode/images/1/1f/Inferno-inglesa2.jpg/revision/latest?cb=20130804204102'),
('Deception Point', '2001-10-25', 'Novel', 'http://danbrown.com/wp-content/themes/danbrown/images/db/covers/dp.jpg'),
('Lost Symbol', '2009-12-05', 'Novel', 'https://upload.wikimedia.org/wikipedia/en/0/07/LostSymbol.jpg'),
('The Da Vinci Code', '2003-02-09', 'Novel', 'https://upload.wikimedia.org/wikipedia/en/6/6b/DaVinciCode.jpg'),
('The Cave', '1999-04-18', 'Fantasy', 'http://jamesrollins.com/wp-content/uploads/book_1999_subterranean_russia.jpg'),
('The Deep', '2001-07-28', 'Fantasy', 'http://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1385418897i/18950831._UY200_.jpg'),
('The eye of God', '2013-11-06', 'Fantasy', 'https://mgbookreviews.files.wordpress.com/2013/08/cover-the-eye-of-god.jpg'),
('The Colorado Kid', '2005-03-09', 'Detective Novel', 'https://s-media-cache-ak0.pinimg.com/originals/83/0c/28/830c2893a0637077ae3fee3e67d760c8.jpg'),
('Color of evil', '2005-03-09', 'Horror', 'https://www.bookclub.ua/images/db/goods/38385_57796.jpg'),  
('It', '1986-08-19', 'Horror', 'http://films-online.su/img/2/f33fac7e7de0.jpg'), 
('The Shining', '1977-01-25', 'Novel', 'http://www.asianbooks.ru/pic/tovar/53493_b.jpg'), 
('Under the Dome', '2009-05-09', 'Novel', 'https://upload.wikimedia.org/wikipedia/ru/8/80/Under_the_Dome_first_cover.jpg');

--select * from Book

-----------------------------------------------------------------------------------------------------------------------------------------------------------------

insert into BookRepository(BookID, NormQuantity, RealQuantity)
values
(1, 5, 3),(2, 4, 4),(3, 8, 6),(4, 3, 2), 
(5, 7, 7),(6, 26, 15),(7, 14, 10),
(8, 5, 4),(9, 20, 20),(10, 6, 0),(11, 9, 8),(12, 12, 11),
(13, 17, 15),(14, 23, 22),(15, 11, 9),(16, 8, 6),(17, 15, 13),(18, 7, 0),(19, 12, 8);

--select * from BookRepository

-----------------------------------------------------------------------------------------------------------------------------------------------------------------

insert into IssuingJournal(BookID, ReaderID, IssueDate, ReturnDate)
values
(1, 2, '2017-04-07 12:00:05.000', '2017-04-10 13:25:12.021'),
(1, 1, '2017-04-07 17:08:45.058', null),
(2, 1, '2017-04-09 18:20:01.100', '2017-04-11 10:12:10.122'),
(3, 3, '2017-04-08 16:10:12.050', '2017-04-10 19:45:11.321'),
(2, 2, '2017-04-07 12:00:09.200', null),
(1,3,'2017-04-03 08:05:42.256',null),
(3,3,'2017-04-03 08:05:42.256',null),
(3,2,'2017-04-03 08:05:42.256',null),
(4,6,'2017-03-23 10:05:22.289',null),
(6,1,'2017-03-21 10:05:26.299',null),
(6,2,'2017-03-22 11:08:52.589',null),
(6,3,'2017-03-24 12:15:23.389',null),
(6,4,'2017-03-25 13:00:32.275',null),
(6,5,'2017-03-26 14:06:28.269',null),
(6,6,'2017-03-27 14:35:22.243',null),
(7,7,'2017-04-17 15:32:22.243',null),
(7,5,'2017-04-02 17:55:21.250',null),
(7,2,'2017-04-07 12:35:22.274',null),
(7,3,'2017-03-30 14:55:40.253',null),
(8,2,'2017-02-14 10:57:20.256',null),
(10,1,'2017-04-14 20:34:10.226',null),
(10,2,'2017-03-21 10:11:20.101',null),
(10,3,'2017-02-02 21:22:31.212',null),
(10,4,'2017-03-13 02:33:42.323',null),
(10,5,'2017-04-24 13:44:53.434',null),
(10,6,'2017-02-05 21:55:04.545',null),
(11,7,'2017-03-16 05:06:15.656',null),
(12,1,'2017-04-27 16:17:26.767',null),
(13,2,'2017-02-08 21:28:37.878',null),
(13,3,'2017-03-19 08:39:48.989',null),
(14,4,'2017-04-20 19:40:59.090',null),
(15,5,'2017-02-01 20:51:00.101',null),
(15,6,'2017-03-12 07:02:11.212',null),
(16,7,'2017-04-23 18:13:22.323',null),
(16,1,'2017-02-04 22:24:33.434',null),
(17,2,'2017-03-15 09:35:44.545',null),
(17,3,'2017-04-26 10:46:55.656',null),
(18,4,'2017-02-07 21:57:06.767',null),
(18,5,'2017-03-18 02:08:17.878',null),
(18,6,'2017-04-29 13:19:28.989',null),
(18,7,'2017-02-01 14:20:39.091',null),
(18,1,'2017-03-12 05:31:40.102',null),
(18,2,'2017-04-23 16:42:51.213',null),
(18,3,'2017-02-04 07:53:02.324',null),
(19,4,'2017-03-15 18:04:13.435',null),
(19,5,'2017-04-26 20:15:24.546',null),
(19,6,'2017-03-07 09:26:35.657',null),
(19,7,'2017-02-18 11:37:46.768',null);

--select * from IssuingJournal

-----------------------------------------------------------------------------------------------------------------------------------------------------------------

insert into Roles (RoleID, RoleName)
values 
(1, 'Admin'),
(2, 'User'), 
(3, 'Guest');

-----------------------------------------------------------------------------------------------------------------------------------------------------------------

insert into ReaderRoles (ReaderId, RoleID)
values 
(1, 2),
(2, 1),
(3, 2),
(4, 2),
(5, 2),
(6, 2),
(7, 2);
