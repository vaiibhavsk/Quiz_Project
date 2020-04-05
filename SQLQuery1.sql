create database DB_Qiuz

use DB_Qiuz

create table Tbl_Admin
(
	Ad_ID int identity primary key,
	Ad_Name Nvarchar(20) not null unique,
	Ad_Password Nvarchar(20) not null
)

create table Tbl_Questions
(
	Q_ID int identity primary key,
	Q_Text Nvarchar(Max) Not Null,
	Op_A Nvarchar(20) Not null unique,
	Op_B Nvarchar(20) Not null unique,
	Op_C Nvarchar(20) Not null unique,
	Op_D Nvarchar(20) Not null unique,
	Correct_Op Nvarchar(20) Not null 
)

create table Tbl_Student
(
	S_ID int identity primary key,
	S_UserName Nvarchar(20) not null unique,
	S_Password Nvarchar(20) not null,
	S_FirstName Nvarchar(20) not null,
	S_LastName Nvarchar(20) not null,
	S_EmailID Nvarchar(20) not null,
	S_ContactNo int,
	S_Birthdate date,
	S_Age int,
	S_img Nvarchar(max) not null
)

create table Tbl_SetExam
(
	Exam_ID int identity primary key,
	Exam_Date datetime,
	Exam_FK_Student int foreign key references Tbl_Student(S_ID),
	Exam_Name Nvarchar(50) Not null,
	Exam_StudentScore int
) 

select @@SERVERNAME