/*
ALTER TABLE Category
ADD CONSTRAINT FK_Category_Subcategory_SubcategoryId
FOREIGN KEY (SubcategoryId) REFERENCES Subcategory(Id);
*/




ALTER TABLE Category
DROP CONSTRAINT FK_Category_Subcategory_SubcategoryId;