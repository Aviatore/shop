INSERT INTO dbo.addresses (street,city,country,zipcode) VALUES
	 ('Rigney','Curumaní','Colombia',''),
	 ('Ohio','Panique','Philippines',''),
	 ('Riverside','Hoima','Uganda',''),
	 ('Waywood','Plasy','Czech Republic',''),
	 ('Goodland','København','Denmark',''),
	 ('Sycamore','New York City','United States',''),
	 ('Chive','Malonty','Czech Republic',''),
	 ('Anniversary','Dukštas','Lithuania',''),
	 ('Bluestem','Höganäs','Sweden',''),
	 ('Carey','Sumoto','Japan','');

INSERT INTO dbo.addresses (street,city,country,zipcode) VALUES
	 ('fwrgreg','Warsaw','Poland',''),
	 ('fwrgreg','Warsaw','Poland',''),
	 ('sdgsgsg','Warsaw','Poland','01-095'),
	 ('sdgsgsg','Warsaw','Poland','01-095'),
	 ('sdgffdf','dfdsf','sfgsd','303-098'),
	 ('sdgffdf','dfdsf','sfgsd','303-098'),
	 ('2332','sdfdsf','gsdfgs','123-123'),
	 ('2332','sdfdsf','gsdfgs','123-123'),
	 ('dsdfdsf','fssdaf','ewwesfef','303-123'),
	 ('dsdfdsf','fssdaf','ewwesfef','303-123');

INSERT INTO dbo.addresses (street,city,country,zipcode) VALUES
	 ('efsdf','dsdgsjs','sdfsdf','123-123'),
	 ('efsdf','dsdgsjs','sdfsdf','123-123'),
	 ('sdfsf','fgdgdgfg','fsefsf','123-123'),
	 ('sdfsdfs','Warsaw','Poland','01-905');
	 
INSERT INTO dbo.authors (first_name,last_name) VALUES
	 ('Bruce','Herbert'),
	 ('Mihaela','Noroc'),
	 ('Fernande','Timson'),
	 ('Robin','Campbell'),
	 ('Robert','Kirkman'),
	 ('Bria','Bridgland'),
	 ('Idalina','Bodley'),
	 ('J K','Rowling'),
	 ('Cassandre','Lidstone'),
	 ('Andrew','Troelsen');

INSERT INTO dbo.genres (genre_name) VALUES
	 ('Computing'),
	 ('Travel & Holiday Guides'),
	 ('Fantasy'),
	 ('Historical'),
	 ('Horror'),
	 ('Romance');

INSERT INTO dbo.publishers (publisher_name) VALUES
	 ('Springer Verlag Berlin and Heidelberg GmbH Co KG'),
	 ('Neustar, Inc.'),
	 ('St Martins Press'),
	 ('Penguin Books Ltd'),
	 ('Wordsworth Editions Ltd'),
	 ('Little Brown Book Group');

INSERT INTO dbo.users (user_name,email,phone) VALUES
	 ('Belvia','bvedekhin0@qq.com','324564345'),
	 ('Jacquenetta','jstinson1@ebay.co.uk','345434564'),
	 ('Worden','wclist2@virginia.edu','702125487'),
	 ('Jo','jlemon3@scientificamerican.com','570038230'),
	 ('Roby','rlefriec4@ning.com','256726780'),
	 ('Hamish','hcaze5@cnn.com','802570345'),
	 ('Ignacius','ihachard6@cbc.ca','921117912'),
	 ('Alfie','aovenden7@posterous.com','791109167'),
	 ('Annnora','awaterson8@zimbio.com','660825047'),
	 ('Clair','colongain9@npr.org','875360961');

INSERT INTO dbo.users (user_name,email,phone) VALUES
	 ('Jan Nowak','test@test.pl','123456789'),
	 ('john test','test@test.pl','123456789'),
	 ('adadfa','assf@test.pl','1234567899'),
	 ('sdsdas','aasd@deee.pl','1234567899'),
	 ('sdfdsf','dsfsdf@sdfdf','123456789'),
	 ('asdas','asdasd@dfd.coo','123456789'),
	 ('srgsrg','sgsg@sadasd','123456789'),
	 ('Test Jan','jan@test','987654321');
	 
INSERT INTO dbo.books (title,genre_id,publisher_id,price,description,fig_url) VALUES
	 ('C# (C Sharp Programming) : A Step by Step Guide for Beginners',1,5,109.13,'C# 
Have you always wanted to learn c sharp programming language but are afraid it''ll be too difficult for you? Or perhaps you know other programming languages but are interested in learning C Sharp language fast?
This book is for you. You no longer have to waste your time and money learning C# from boring books that are 600 pages long, expensive online courses or complicated C# tutorials that just leave you more confused. What this book offers... C Sharp for Beginners Complex concepts are broken down into simple steps to ensure that you can easily master the C# language even if you have never coded before. Carefully Chosen C# Examples Examples are carefully chosen to illustrate all concepts. In addition, the output for all examples are provided immediately so you do not have to wait till you have access to your computer to test the examples.','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/6934/9781693465185.jpg'),
	 ('The Atlas of Beauty : Women of the World in 500 Portraits',2,4,98.67,'Photographs and stories of 500 women from around the world, based on the author''s hugely popular website.

Since 2013 Mihaela Noroc has travelled the world with her backpack and camera taking photos of everyday women to showcase the diversity and beauty all around us. The Atlas of Beauty is a collection of her photographs that celebrates women from fifty countries across the globe and shows that beauty is everywhere, regardless of money, race or social status, and comes in many different sizes and colours. Mihaela''s portraits feature women in their native environments, from the Amazon rain forest to markets in India, London city streets and parks in Harlem, creating a mirror of our varied cultures and proving that beauty has no rules.

''Stunning . . . aims to challenge the ideals of beauty dictated by the women''s fashion magazine industry'' Independent','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/8461/9781846149412.jpg'),
	 ('The House in the Cerulean Sea',3,3,70.1,'Linus Baker leads a quiet, solitary life. At forty, he lives in a tiny house with a devious cat and his old records. As a Case Worker at the Department in Charge Of Magical Youth, he spends his days overseeing the well-being of children in government-sanctioned orphanages.

When Linus is unexpectedly summoned by Extremely Upper Management he''s given a curious and highly classified assignment: travel to Marsyas Island Orphanage, where six dangerous children reside: a gnome, a sprite, a wyvern, an unidentifiable green blob, a were-Pomeranian, and the Antichrist. Linus must set aside his fears and determine whether or not they''re likely to bring about the end of days.

But the children aren''t the only secret the island keeps. Their caretaker is the charming and enigmatic Arthur Parnassus, who will do anything to keep his wards safe. As Arthur and Linus grow closer, long-held secrets are exposed, and Linus must make a choice: destroy a home or watch the world burn.','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/2502/9781250217318.jpg'),
	 ('Letters from a Stoic : Epistulae Morales Ad Lucilium',4,2,44.67,'''It is philosophy that has the duty of protecting us ... without it no one can lead a life free of fear or worry'' 

For several years of his turbulent life, in which he was dogged by ill health, exile and danger, Seneca was the guiding hand of the Roman Empire. This selection of Seneca''s letters shows him upholding the ideals of Stoicism - the wisdom of the self-possessed person immune to life''s setbacks - while valuing friendship and courage, and criticizing the harsh treatment of slaves and the cruelties in the gladiatorial arena. The humanity and wit revealed in Seneca''s interpretation of Stoicism is a moving and inspiring declaration of the dignity of the individual mind. ','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/1404/9780140442106.jpg'),
	 ('The Walking Dead Volume 5: The Best Defense',5,1,61.34,'The world we knew is gone. The world of commerce and 
frivolous necessity has been replaced by a world of survival and responsibility. 
An epidemic of apocalyptic proportions has swept the globe, causing the dead to 
rise and feed on the living. In a matter of months society has crumbled: no 
government, no grocery stores, no mail delivery, no cable TV. In a world ruled 
by the dead, the survivors are forced to finally start living. 
As the survivors settle into their prison home something has drawn them out 
into the open... out of the prison... out of their sanctuary. This is a major 
turning point for the overall story of The Walking Dead, setting the 
stage for years to come. 
Reprint Edition','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/5824/9781582406121.jpg'),
	 ('Peace Talks : The Dresden Files, Book Sixteen',5,2,39.9,'HARRY DRESDEN IS BACK AND READY FOR ACTION, in the new entry in the #1 New York Times bestselling Dresden Files. 

When the Supernatural nations of the world meet up to negotiate an end to ongoing hostilities, Harry Dresden, Chicago''s only professional wizard, joins the White Council''s security team to make sure the talks stay civil. But can he succeed, when dark political manipulations threaten the very existence of Chicago - and all he holds dear? ','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/3565/9780356515298.jpg'),
	 ('The Second World War',4,3,43.57,'A magisterial, single-volume history of the greatest conflict the world has ever known by our foremost military historian.

The Second World War began in August 1939 on the edge of Manchuria and ended there exactly six years later with the Soviet invasion of northern China. The war in Europe appeared completely divorced from the war in the Pacific and China, and yet events on opposite sides of the world had profound effects. Using the most up-to-date scholarship and research, Beevor assembles the whole picture in a gripping narrative that extends from the North Atlantic to the South Pacific and from the snowbound steppe to the North African Desert.

Although filling the broadest canvas on a heroic scale, Beevor''s The Second World War never loses sight of the fate of the ordinary soldiers and civilians whose lives were crushed by the titanic forces unleashed in this, the most terrible war in history.','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/7802/9781780225647.jpg'),
	 ('Harry Potter and the Goblet of Fire',3,3,51.15,'The Triwizard Tournament is to be held at Hogwarts. Only wizards who are over seventeen are allowed to enter - but that doesn''t stop Harry dreaming that he will win the competition. Then at Hallowe''en, when the Goblet of Fire makes its selection, Harry is amazed to find his name is one of those that the magical cup picks out. He will face death-defying tasks, dragons and Dark wizards, but with the help of his best friends, Ron and Hermione, he might just make it through - alive! 

These new editions of the classic and internationally bestselling, multi-award-winning series feature instantly pick-up-able new jackets by Jonny Duddle, with huge child appeal, to bring Harry Potter to the next generation of readers. It''s time to PASS THE MAGIC ON ... ','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/4088/9781408855683.jpg'),
	 ('Rick Stein''s India',2,2,78.99,'Whenever I hear the word curry, I''m filled with a longing for spicy hot food with the fragrance of cumin, cloves and cinnamon. I see deep red colours from lots of Kashmiri chillis, tinged with a suggestion of yellow from turmeric. I think of the tandoor oven, and slightly scorched naan shining with ghee and garlic. When Indians talk of their food, they talk about their life. To understand this country, you need to understand curry. What makes a good curry? Sensual spicy aromas or thick, creamy sauces? Rich, dark dals or crispy fried street snacks? Rick journeys through India to find the answer, searching this colourful, chaotic nation in search of the truths behind our love affair with its food.','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/8499/9781849905787.jpg'),
	 ('Pro C# 5.0 and the .NET 4.5 Framework',1,1,93.39,'This new edition of Pro C# 5.0 and the .NET 4.5 Platform has been completely revised and rewritten to reflect the latest changes to the C# language specification and new advances in the .NET Framework. You''ll find new chapters covering all the important new features that make .NET 4.5 the most comprehensive release yet, including:



.NET APIs for Windows 8 style UI apps 
New asynchronous task-based model for async operations 
How HTML5 support is being wrapped into C# web applications 
New programming interfaces for HTTP applications, including improved IPv6 support 
Expanded WPF, WCF and WF libraries giving C# more power than ever before 

This comes on top of award winning coverage of core C# features, both old and new, that have made the previous editions of this book so popular (you''ll find everything from generics to pLINQ covered here).','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/4302/9781430242338.jpg');

INSERT INTO dbo.books (title,genre_id,publisher_id,price,description,fig_url) VALUES
	 ('Bridgerton: When He Was Wicked: Inspiration for the Netflix Original Series Bridgerton',6,6,38.45,'The sixth novel in Julia Quinn''s globally beloved and bestselling Bridgerton Family series, set in Regency times and now a series created by Shonda Rhimes for Netflix. Welcome to Francesca''s story . . . 
In every life there is a turning point. A moment so tremendous, so sharp and breath-taking, that one knows one''s life will never be the same. For Michael Stirling, London''s most infamous bachelor, that moment came the first time he laid eyes on Francesca Bridgerton.
After a lifetime of chasing women, of smiling slyly as they chased him, of allowing himself to be caught but never permitting his heart to become engaged, he took one look at Francesca Bridgerton and fell so fast and hard into love it was a wonder he managed to remain standing.

Unfortunately for Michael, however, Francesca''s surname was to remain Bridgerton for only a mere thirty-six hours longer - the occasion of their meeting was, lamentably, a supper celebrating her imminent wedding to his cousin . . .','https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/3494/9780349429472.jpg');

INSERT INTO dbo.author_book (book_id,author_id) VALUES
	 (1,1),
	 (1,2),
	 (2,7),
	 (3,1),
	 (4,2),
	 (5,3),
	 (6,4),
	 (7,9),
	 (8,6),
	 (10,4);

INSERT INTO dbo.author_book (book_id,author_id) VALUES
	 (10,5);

INSERT INTO dbo.orders (billing_address_id,shipping_address_id,user_id,payment) VALUES
	 (3,2,2,0),
	 (2,3,3,1),
	 (3,4,5,1),
	 (5,6,1,0),
	 (4,7,2,0),
	 (4,4,2,1),
	 (8,8,9,0),
	 (6,2,3,0),
	 (11,12,11,0),
	 (13,14,12,0);

INSERT INTO dbo.orders (billing_address_id,shipping_address_id,user_id,payment) VALUES
	 (15,16,13,0),
	 (17,18,14,0),
	 (19,20,15,1),
	 (21,22,16,0),
	 (23,23,17,1),
	 (24,24,18,1);

INSERT INTO dbo.books_ordered (order_id,book_id) VALUES
	 (2,2),
	 (2,5),
	 (2,3),
	 (4,1),
	 (4,5),
	 (4,6),
	 (4,4),
	 (5,7),
	 (6,2),
	 (6,1);

INSERT INTO dbo.books_ordered (order_id,book_id) VALUES
	 (6,3),
	 (7,4),
	 (10,8),
	 (10,3),
	 (10,9),
	 (11,1),
	 (11,5),
	 (12,1),
	 (12,3),
	 (13,1);

INSERT INTO dbo.books_ordered (order_id,book_id) VALUES
	 (13,2),
	 (14,1),
	 (14,2),
	 (14,4),
	 (14,9),
	 (15,1),
	 (15,2),
	 (16,1),
	 (16,2);

INSERT INTO dbo.logs (order_id,msg,[timestamp]) VALUES
	 (4,'Order data accepted','2021-01-18 10:34:09.0000000'),
	 (4,'Start payment','2021-01-18 10:36:09.0000000'),
	 (4,'Payment accepted','2021-01-18 10:37:09.0000000'),
	 (4,'Sent order confirmation','2021-01-18 10:38:09.0000000'),
	 (2,'Order data accepted','2021-01-19 11:34:09.0000000'),
	 (2,'Start payment','2021-01-19 11:35:09.0000000'),
	 (7,'Order data accepted','2021-01-21 00:34:09.0000000'),
	 (7,'Start payment','2021-01-21 00:38:09.0000000'),
	 (7,'Payment accepted','2021-01-21 00:43:09.0000000');