-- Assign Foreign keys after table creation


-- in Subcategory
/*
ALTER TABLE Subcategory
ADD CONSTRAINT FK_Category
FOREIGN KEY (CategoryId) REFERENCES Category(Id);
*/


-- in Contact
/*
ALTER TABLE Contact
ADD CONSTRAINT FK_CCategory
FOREIGN KEY (CategoryId) REFERENCES Category(Id);
*/



ALTER TABLE Contact
ADD CONSTRAINT FK_Subcategory
FOREIGN KEY (SubcategoryId) REFERENCES Subcategory(Id);





