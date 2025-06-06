CREATE DATABASE Acervo;
GO

USE Acervo;
GO

CREATE SCHEMA AcervoSchema;
GO

-- CREATES ###########################

CREATE TABLE AcervoSchema.Usuario
( 
    Id INT IDENTITY(1,1),
    Nome NVARCHAR(50),
	Email NVARCHAR(50)
    CONSTRAINT PK_Usuario PRIMARY KEY(Id)
)

CREATE TABLE AcervoSchema.Genero
( 
    Id INT IDENTITY(1,1),
    Nome NVARCHAR(50)
    CONSTRAINT PK_Genero PRIMARY KEY(Id)
)

CREATE TABLE AcervoSchema.Streaming
( 
    Id INT IDENTITY(1,1),
    Nome NVARCHAR(50)
    CONSTRAINT PK_Streaming PRIMARY KEY(Id)
)

CREATE TABLE AcervoSchema.Filme
( 
    Id INT IDENTITY(1,1),
    Nome NVARCHAR(50),
    Genero_Id INT,
    AnoLancamento INT,
    Imagem varchar(255),
	Diretor varchar(255),
    CONSTRAINT PK_Filme PRIMARY KEY(Id),
	CONSTRAINT FK_Filme_Genero_Id FOREIGN KEY(Genero_Id) REFERENCES AcervoSchema.Genero(Id)
)

--Tabela Relacional de Filmes com seus streamings
CREATE TABLE AcervoSchema.FilmeStreaming
( 
    Id INT IDENTITY(1,1),
    Filme_Id INT,
	Streaming_Id INT,
    CONSTRAINT PK_FilmeStreaming PRIMARY KEY(Id),
	CONSTRAINT FK_FilmeStreaming_Filme_Id FOREIGN KEY(Filme_Id) REFERENCES AcervoSchema.Filme(Id),
	CONSTRAINT FK_FilmeStreaming_Streaming_Id FOREIGN KEY(Streaming_Id) REFERENCES AcervoSchema.Streaming(Id)
)


CREATE TABLE AcervoSchema.Comentario
( 
    Id INT IDENTITY(1,1),
    Filme_Id INT,
    Nota INT,
    Observacao VARCHAR(400),
	Usuario_Id INT,
    CONSTRAINT PK_Filme_Comentarios PRIMARY KEY(Id),

	--ForeignKey
    CONSTRAINT FK_Filme_Comentario_FilmeId FOREIGN KEY(Filme_Id) REFERENCES AcervoSchema.Filme(Id)
    ON DELETE CASCADE,

	CONSTRAINT FK_Filme_Comentario_Usuario_Id FOREIGN KEY(Usuario_Id) REFERENCES AcervoSchema.Usuario(Id),

	--Limita nota de 0 a 5
	CONSTRAINT CHK_Nota CHECK (Nota >= 0 AND Nota <= 5)
)



-- INSERTS $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
INSERT INTO AcervoSchema.Usuario
  (Nome, Email)
VALUES
	('João Pedro', 'joao@hotmail.com'),
	('Maria Carla', 'maria@hotmail.com'),
	('Marcelo Gomes', 'marcelo@hotmail.com'),
	('Luna Clair', 'luna@hotmail.com'),
	('Henrique Barbosa', 'henrique@hotmail.com');


INSERT INTO AcervoSchema.Genero
  (Nome)
VALUES
	('Ação'),
	('Aventura'),
	('Terror'),
	('Suspense'),
	('Comédia');


INSERT INTO AcervoSchema.Streaming
  ( Nome)
VALUES
	('Netflix'),
	('HBO Max'),
	('Disney Plus'),
	('Prime Video');


INSERT INTO AcervoSchema.Filme
  (Nome, Genero_Id, AnoLancamento, Imagem, Diretor )
VALUES
    ('Prenda-me Se For Capaz', 1, 2002,'https://br.web.img3.acsta.net/pictures/210/100/21010048_20130603234956231.jpg', 'Michael Bay'),
    ('Chamas da Vingança', 1, 2004,'https://br.web.img3.acsta.net/medias/nmedia/18/96/28/47/20457373.jpg', 'Michael Bay'),
    ('Senhor dos Anéis: A Sociedade do Anel', 2, 2001, 'https://br.web.img3.acsta.net/medias/nmedia/18/92/91/32/20224832.jpg', 'Jhon Snow'), 
    ('Harry Potter e a Pedra Filosofal', 2, 2003, 'https://ingresso-a.akamaihd.net/img/cinema/cartaz/7766-cartaz.jpg', 'Marie Louise');


INSERT INTO AcervoSchema.FilmeStreaming
  (Filme_Id, Streaming_Id)
VALUES
	(5, 1),
	(5, 2),
	(5, 2),
	(5, 4),
	(6, 3),
	(6, 4),
	(7, 1),
	(8, 4);



INSERT INTO AcervoSchema.Comentario
  ( Filme_Id, Nota, Observacao, Usuario_Id )
VALUES
    (5, 5,'Gostei muito do filme', 1),
    (5, 1,'Péssima atuação', 1),
    (6, 5,'Amei se tornou meu favorito', 2),
    (7, 3,'Prefiro o outro filme dele O Protetor', 2),
    (8, 4,'Excepcional filme. Direção magistral, tudo incrível: roteiro, atores etc.', 2);


--SELECTS $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

SELECT 
	fil.Id, 
	fil.Nome,
	fil.Genero_Id,
	gen.Nome AS GeneroNome,
	fil.AnoLancamento,
	fil.Imagem, 
	fil.Diretor,
	str.Nome AS StreamingNome -- Nome do serviço de streaming
  FROM AcervoSchema.Filme fil
  JOIN AcervoSchema.Genero gen ON gen.id = fil.Genero_Id
  JOIN AcervoSchema.FilmeStreaming filstr ON filstr.Filme_Id = fil.Id
  JOIN AcervoSchema.Streaming str ON str.Id = filstr.Streaming_Id;
GO