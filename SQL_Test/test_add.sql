DELETE FROM Category WHERE Name='krokiet';
DELETE FROM Subcategory WHERE Name='masło';
DELETE FROM Contact WHERE Name='budyń';

INSERT INTO Category (Name)
VALUES ('zdrowe');

INSERT INTO Subcategory (Name, CategoryId)
VALUES ('warzywa', 1);

INSERT INTO Contact (Name, Surname, Email, Password, CategoryId, SubcategoryId, Phone, BirthDate)
VALUES ('brokuły', 'hortex', 'brokul@hortex.pl', 'W@rzywo123', 1, 1, '111-111-111', '2000-10-22');

SELECT * FROM Category;
SELECT * FROM Subcategory;
SELECT * FROM Contact;