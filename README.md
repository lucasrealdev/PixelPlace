# PixelPlace
Projeto Web com C# .NET Framework, MySQL, Design Patterns, API RestFull, e Metodologia Scrum  ||  Android -  Java. 


#Create DataBase
CREATE DATABASE `pixelplace` /*!40100 DEFAULT CHARACTER SET utf8 */;

#Create Usuario
CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `NomeUser` varchar(45) NOT NULL,
  `UrlImage` varchar(45) DEFAULT NULL,
  `Email` varchar(45) NOT NULL,
  `Senha` varchar(45) NOT NULL,
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

#Create Loja
CREATE TABLE `loja` (
  `Categoria` varchar(45) NOT NULL,
  `Jogo_idJogo` int(11) NOT NULL,
  KEY `fk_Loja_Jogo1` (`Jogo_idJogo`),
  CONSTRAINT `fk_Loja_Jogo1` FOREIGN KEY (`Jogo_idJogo`) REFERENCES `jogo` (`idJogo`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#Create Jogo
CREATE TABLE `jogo` (
  `idJogo` int(11) NOT NULL,
  `nome` varchar(45) NOT NULL,
  `urlCapa` varchar(45) NOT NULL,
  `descricao` varchar(45) NOT NULL,
  `categoria` varchar(45) NOT NULL,
  `preco` double NOT NULL,
  `desconto` varchar(45) DEFAULT NULL,
  `data` date NOT NULL,
  PRIMARY KEY (`idJogo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#Create Biblioteca 
CREATE TABLE `jogo` (
  `idJogo` int(11) NOT NULL,
  `nome` varchar(45) NOT NULL,
  `urlCapa` varchar(45) NOT NULL,
  `descricao` varchar(45) NOT NULL,
  `categoria` varchar(45) NOT NULL,
  `preco` double NOT NULL,
  `desconto` varchar(45) DEFAULT NULL,
  `data` date NOT NULL,
  PRIMARY KEY (`idJogo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
