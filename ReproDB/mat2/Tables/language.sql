CREATE TABLE [mat2].[language]
(
	[language] INT NOT NULL PRIMARY KEY,
    [rfc_1766] NCHAR(5) NOT NULL,
    [text] NVARCHAR(255) NULL,
)
