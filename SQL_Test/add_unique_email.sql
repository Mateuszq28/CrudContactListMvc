﻿-- My SQL
/*
ALTER TABLE Contact
ADD UNIQUE (Email);
*/


-- SQL SERVEER
/*
ALTER TABLE Contact
ADD CONSTRAINT AC_Contact UNIQUE (Email);
*/


--CREATE UNIQUE INDEX IX_Contact_Email
--ON Contact (Email);


ALTER TABLE Contact
DROP COLUMN Email;

ALTER TABLE Contact
ADD Email NVARCHAR (320) UNIQUE NOT NULL;

