﻿DBCC CHECKIDENT ('[Category]', RESEED, 0);
DBCC CHECKIDENT ('[Subcategory]', RESEED, 0);
DBCC CHECKIDENT ('[Contact]', RESEED, 0);


-- My SQL
-- ALTER TABLE Subcategory AUTO_INCREMENT = 1;