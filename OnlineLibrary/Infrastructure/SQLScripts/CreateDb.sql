CREATE TABLE Book
(
    BookId      bigint IDENTITY(1,1) NOT NULL,
    Name        nvarchar(255)        NOT NULL,
    PublishDate date                 NOT NULL,
    Genre       nvarchar(255)        NULL,
    ImageUrl    nvarchar(255)        NULL,
    
    CONSTRAINT PK_BookId PRIMARY KEY (BookId)
);

CREATE TABLE Author
(
    AuthorId  bigint IDENTITY(1,1) NOT NULL,
    FirstName nvarchar(255)        NOT NULL,
    LastName  nvarchar(255)        NOT NULL,
    NickName  nvarchar(255)        NULL,
    BirthDate date                 NULL,
    PhotoUrl  nvarchar(255)        NULL,
    
    CONSTRAINT PK_AuthorId PRIMARY KEY (AuthorId)
);

CREATE TABLE Reader
(
    ReaderId  bigint IDENTITY(1,1) NOT NULL,
    FirstName nvarchar(255)        NOT NULL,
    LastName  nvarchar(255)        NOT NULL,
    NickName  nvarchar(255)        NOT NULL,
    Email     nvarchar(255)        NOT NULL,
    
    CONSTRAINT PK_UserId PRIMARY KEY (ReaderId)
);

CREATE TABLE BookRepository
(
    BookID       bigint       NOT NULL,
    NormQuantity int          NOT NULL,
    RealQuantity int          NOT NULL,
    
    CONSTRAINT PK_BookRepository PRIMARY KEY (BookID)
);

CREATE TABLE BookAuthors
(
    BookID        bigint  NOT NULL,
    AuthorID      bigint  NOT NULL,
    
    CONSTRAINT PK_BookAuthorsCode PRIMARY KEY (BookID, AuthorID)
);

CREATE TABLE IssuingJournal
(
    IssueId         bigint IDENTITY(1,1) NOT NULL,
    BookID          bigint               NOT NULL,
    ReaderId        bigint               NOT NULL,
    IssueDate       datetime             NOT NULL,
    ReturnDate      datetime             NULL,
    
    CONSTRAINT PK_IssuingJournalCode PRIMARY KEY (IssueId)   
);

create table Roles 
(
RoleID   bigint    not null, 
RoleName char(30)  not null, 

CONSTRAINT PK_RoleID PRIMARY KEY (RoleID)
);

create table ReaderRoles
(
ReaderId bigint not null, 
RoleID   bigint not null, 

CONSTRAINT PK_ReaderRoles PRIMARY KEY (ReaderId, RoleID)
);


