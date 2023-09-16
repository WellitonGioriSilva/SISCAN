create database bd_siscan;
use bd_siscan;

create table Produto
(
id_prod int primary key auto_increment,
nome_prod varchar(45),
marca_prod varchar(45),
tipo_prod varchar(45),
valor_com_prod double,
valor_ven_prod double,
visivel_prod varchar(10)
);

create table Caixa
(
id_cai int primary key auto_increment,
data_cai date,
hora_abertura_cai time,
hora_fechamento_cai time,
valor_inicial_cai double,
valor_final_cai double,
visivel_cai varchar(10)
);

create table Funcao(
id_fun int primary key auto_increment,
nome_fun varchar(45),
salario_fun double,
turno_fun varchar(45),
nivel_acess_fun int,
visivel_fun varchar(10)
);

create table Forma_Pagamento
(
id_form_pag int primary key auto_increment,
nome_form_pag varchar(45),
visivel_form_pag varchar(10)
);

create table Estado
(
 id_est int primary key auto_increment,
 nome_est varchar(45),
 sigla_est varchar(45),
 uf_est int,
 visivel_est varchar(10)
);

create table Cidade
(
id_cid int primary key auto_increment,
nome_cid varchar(45),
visivel_cid varchar(10),
id_est_fk int,
foreign key (id_est_fk) references Estado (id_est) 
);

create table Fornecedor
(
id_forn int primary key auto_increment,
razao_social_forn varchar(45),
cnpj_forn varchar(45),
bairro_forn varchar(45),
rua_forn varchar(45),
nome_fantasia_forn varchar(45),
telefone_forn varchar(45),
inscricao_estadual_forn varchar(45),
responsavel_forn varchar(45),
visivel_forn varchar(10),
id_cid_fk int,
foreign key (id_cid_fk) references Cidade (id_cid) 
);

create table Cliente
(
id_cli int primary key auto_increment,
nome_cli varchar(45),
cpf_cli varchar(45),
email_cli varchar(45),
sexo_cli varchar(45),
data_nascimento_cli date,
rua_cli varchar(45),
bairro_cli varchar(45),
numero_cli int,
visivel_cli varchar(10),
id_cid_fk int,
foreign key (id_cid_fk) references Cidade (id_cid) 
);

create table Funcionario(
id_func int primary key auto_increment,
nome_func varchar(45),
bairro_func varchar(45),
rua_func varchar(45),
cpf_func varchar(45),
numero_func int,
sexo_func varchar(45),
nivel_acess_func int,
visivel_func varchar(10),
id_cid_fk int,
foreign key (id_cid_fk) references Cidade (id_cid),
id_fun_fk int,
foreign key (id_fun_fk) references Funcao (id_fun)
);

SELECT * FROM Funcionario RIGHT JOIN Cidade ON Funcionario.id_cid_fk = Cidade.id_cid INNER JOIN Funcao ON Funcionario.id_fun_fk = Funcao.id_fun;

create table Venda
(
id_vend int primary key auto_increment,
data_vend date,
hora_vend time,
valor_vend double,
visivel_vend varchar(10),
id_func_fk int,
foreign key (id_func_fk) references Funcionario (id_func) 
);

create table Venda_produto
(
id_vend_prod int primary key auto_increment,
quantidade_vend_prod int,
visivel_vend_prod varchar(10),
id_prod_fk int,
foreign key (id_prod_fk) references produto (id_prod),
id_vend_fk int,
foreign key (id_vend_fk) references Venda (id_vend)
);

create table Estoque
(
id_est int primary key auto_increment,
lote_est varchar(45),
quantidade_est double,
validade_est date,
visivel_est varchar(10),
id_prod_fk int,
foreign key (id_prod_fk) references produto (id_prod)
);

create table Compra
(
id_com int primary key auto_increment,
valor_com double,
data_com date,
visivel_com varchar(10),
id_forn_fk int,
foreign key (id_forn_fk) references Fornecedor (id_forn)
);

create table Compra_produto
(
id_com_prod int primary key auto_increment,
quantidade_com_prod int,
visivel_com_prod varchar(10),
id_com_fk int,
foreign key (id_com_fk) references Compra (id_com),
id_prod_fk int,
foreign key (id_prod_fk) references produto (id_prod)
);

create table Despesa
(
id_desp int primary key auto_increment,
nome_desp varchar(45),
parcelas_desp int,
valor_desp double,
data_desp varchar(45),
vencimento_desp varchar(45),
status_desp varchar(45),
visivel_desp varchar(10),
id_com_fk int,
foreign key (id_com_fk) references Compra (id_com)
);

create table Pagamento
(
id_pag int primary key auto_increment,
data_pag date, 
valor_pag double,
hora_pag time,
visivel_pag varchar(10),
id_desp_fk int,
foreign key (id_desp_fk) references Despesa (id_desp),
id_cai_fk int,
foreign key (id_cai_fk) references Caixa (id_cai),
id_form_pag_fk int,
foreign key (id_form_pag_fk) references Forma_Pagamento (id_form_pag)
);

create table Recebimento
(
id_rec int primary key auto_increment,
data_rec date,
valor_rec double,
hora_rec time,
visivel_rec varchar(10),
id_vend_fk int,
foreign key (id_vend_fk) references Venda (id_vend),
id_cai_fk int,
foreign key (id_cai_fk) references Caixa (id_cai),
id_form_pag_fk int,
foreign key (id_form_pag_fk) references Forma_Pagamento (id_form_pag)
);

create table Usuario
(
id_usu int primary key auto_increment,
usuario_usu varchar(45),
senha_usu varchar(45),
nivel_acess_usu int,
visivel_usu varchar(10),
id_func_fk int,
foreign key (id_func_fk) references Funcionario (id_func) 
);

#Inserts
INSERT INTO Forma_Pagamento VALUES(null, 'Cartão de Crédito', "Sim"),
(null, 'Cartão de Débito', "Sim"),
(null, 'Pix', "Sim");

INSERT INTO Estado VALUES(null, "Rôndonia", "RO", 11, "Sim");

INSERT INTO Cidade VALUES
(null, 'Alta Floresta d''Oeste', "Sim", 1),
(null, 'Ariquemes',"Sim", 1),
(null, 'Cabixi',"Sim", 1),
(null, 'Cacoal',"Sim", 1),
(null, 'Cerejeiras',"Sim", 1),
(null, 'Colorado do Oeste',"Sim", 1),
(null, 'Corumbiara',"Sim", 1),
(null, 'Costa Marques',"Sim", 1),
(null, 'Espigão d''Oeste',"Sim", 1),
(null, 'Governador Jorge Teixeira',"Sim", 1),
(null, 'Guajará-Mirim',"Sim", 1),
(null, 'Jaru',"Sim", 1),
(null, 'Ji-Paraná',"Sim", 1),
(null, 'Machadinho d''Oeste',"Sim", 1),
(null, 'Nova Brasilândia d''Oeste',"Sim", 1),
(null, 'Ouro Preto do Oeste',"Sim", 1),
(null, 'Pimenta Bueno',"Sim", 1),
(null, 'Porto Velho',"Sim", 1),
(null, 'Presidente Médici',"Sim", 1),
(null, 'Rio Crespo',"Sim", 1),
(null, 'Rolim de Moura',"Sim", 1),
(null, 'Santa Luzia d''Oeste',"Sim", 1),
(null, 'Vilhena',"Sim", 1),
(null, 'São Miguel do Guaporé',"Sim", 1),
(null, 'Nova Mamoré',"Sim", 1),
(null, 'Alvorada d''Oeste',"Sim", 1),
(null, 'Alto Alegre dos Parecis',"Sim", 1),
(null, 'Alto Paraíso',"Sim", 1),
(null, 'Buritis',"Sim", 1),
(null, 'Novo Horizonte do Oeste',"Sim", 1),
(null, 'Cacaulândia',"Sim", 1),
(null, 'Campo Novo de Rondônia',"Sim", 1),
(null, 'Candeias do Jamari',"Sim", 1),
(null, 'Castanheiras',"Sim", 1),
(null, 'Chupinguaia',"Sim", 1),
(null, 'Cujubim',"Sim", 1),
(null, 'Governador Jorge Teixeira',"Sim", 1),
(null, 'Itapuã do Oeste',"Sim", 1),
(null, 'Ministro Andreazza',"Sim", 1),
(null, 'Mirante da Serra',"Sim", 1),
(null, 'Monte Negro',"Sim", 1),
(null, 'Nova União',"Sim", 1),
(null, 'Parecis',"Sim", 1),
(null, 'Pimenteiras do Oeste',"Sim", 1),
(null, 'Primavera de Rondônia',"Sim", 1),
(null, 'São Felipe d''Oeste',"Sim", 1),
(null, 'São Francisco do Guaporé',"Sim", 1),
(null, 'Seringueiras',"Sim", 1),
(null, 'Teixeirópolis',"Sim", 1),
(null, 'Theobroma',"Sim", 1),
(null, 'Urupá',"Sim", 1),
(null, 'Vale do Anari',"Sim", 1),
(null, 'Vale do Paraíso',"Sim", 1);

#Procedimentos

#Procedimento - Caixa
DELIMITER $$
CREATE PROCEDURE InsertCaixa(dataCad date, horaAbe time, horaFec time, valorIni double, valorFin double, out msg varchar(100))
BEGIN
DECLARE dataAtual date;
IF(dataCad = CURDATE()) THEN
	IF(horaFec > horaAbe) THEN
		INSERT INTO caixa VALUES(null, dataCad, horaAbe, horaFec, valorIni, valorFin, 'Sim');
        SET msg = 'Dados inseridos com sucesso!';
	ELSE
		SET msg = 'O horário de fechamento deve ser após o de abertura!';
	END IF;
ELSE
	SET msg = 'A data de abertura deve ser a data atual!';
END IF;
END;
$$ DELIMITER ;

#CALL InsertCaixa('2023-09-20', '08:00:00', '13:00:00', 150, 750, @ResultCaixa1);
#CALL InsertCaixa('2023-09-20', '14:00:00', '17:30:00', 750, 1250, @ResultCaixa2);
#CALL InsertCaixa('2023-09-20', '18:00:00', '21:00:00', 1250, 3250, @ResultCaixa3);
#SELECT @ResultCaixa1;
#SELECT @ResultCaixa2;
#SELECT @ResultCaixa3;

#Procedimento - Produto
DELIMITER $$
CREATE PROCEDURE InsertProduto(nome varchar(100), marca varchar(100), tipo varchar(100), valorCom double, out msg varchar(100))
BEGIN
DECLARE VerificadorProduto int;
DECLARE ValorVen double;
IF ((nome <> '') AND (marca <> '') AND (tipo <> '') AND (valorCom <> '')) THEN
	SELECT COUNT(id_prod) into VerificadorProduto from produto where nome = nome_prod;
    IF(VerificadorProduto = 0) THEN 
		SET valorVen = valorCom + ((valorCom * 60) / 100);
		INSERT INTO PRODUTO VALUES (null, nome, marca, tipo, valorCom, valorVen, 'Sim');
        SET msg = 'Produto salvo com sucesso!';
    ELSE
		SET msg = 'O produto já existe no sitema!';
	END IF;
ELSE
	SET msg = 'Todos os campos devem estar preenchidos!';
END IF; 
END;
$$ DELIMITER ;

#CALL InsertProduto('Coca-Cola 2L', 'Coca-Cola', 'Bebida', 7, @ResultProduto1);
#CALL InsertProduto('Coxinha de Frango', 'Serve-Bem', 'Salgado', 2.50, @ResultProduto2);
#CALL InsertProduto('Kit-Kat', 'Kit-Kat', 'Doce', 2, @ResultProduto3);
#SELECT @ResultProduto1;
#SELECT @ResultProduto2;
#SELECT @ResultProduto3;

#Procedimento - Compra
DELIMITER $$
CREATE PROCEDURE InsertCompra(valor double, dataComp date, id_fk int, out msg varchar(100))
BEGIN
DECLARE Verificador_fk int;
IF ((valor <> '') AND (dataComp <> '') AND (id_fk <> '')) THEN
	SELECT COUNT(id_forn) into Verificador_fk from fornecedor where id_forn = id_fk;
	IF(Verificador_fk = 1) THEN
		INSERT INTO Compra VALUES(null, valor, dataComp, 'Sim', id_fk);
        SET msg = 'Compra salva com sucesso!'; 
	ELSE
		SET msg = 'O fornecedor informado não existe no sistema!';
	END IF;
ELSE
	SET msg = 'Todos os campos devem estar preenchidos!';
END IF;
END;
$$ DELIMITER ;

#CALL InsertCompra(750, '2023-10-12', 1, @ResultCompra1);
#CALL InsertCompra(1050, '2023-11-10', 2, @ResultCompra2);
#CALL InsertCompra(890, '2023-12-09', 3, @ResultCompra3);
#SELECT @ResultCompra1;
#SELECT @ResultCompra2;
#SELECT @ResultCompra3;

#Procedimento - Função
DELIMITER $$
CREATE PROCEDURE InsertFuncao(nome varchar(100), salario double, turno varchar(100), nivel_acess varchar(100), out msg varchar(100))
BEGIN
DECLARE VerificadorFuncao INT;
IF((nome <> '') AND (salario <> '') AND (turno <> '') AND (nivel_acess <> '')) THEN
	SELECT COUNT(id_fun) INTO VerificadorFuncao FROM Funcao WHERE nome = nome_fun;
    IF(VerificadorFuncao = 0) THEN
		INSERT INTO Funcao VALUES(null, nome, salario, turno, nivel_acess, 'Sim');
        SET msg = 'Função cadastrada com sucesso!';
    ELSE
		SET msg = 'A função já existe no sistema!';
    END IF;
ELSE
	SET msg = 'Todos os campos devem estar preenchidos!';
END IF;
END;
$$ DELIMITER ;

#CALL InsertFuncao('Gerente', '10000', 'Matutino, Vespertino', 3, @ResultFuncao1);
#CALL InsertFuncao('Vendedor', '1500', 'Matutino, Vespertino', 1, @ResultFuncao2);
#CALL InsertFuncao('Estoquista', '2750', 'Matutino, Vespertino', 2, @ResultFuncao3);
#SELECT @ResultFuncao1;
#SELECT @ResultFuncao2;
#SELECT @ResultFuncao3;

#Procedimento - Estoque
DELIMITER $$
CREATE PROCEDURE InsertEstoque(lote varchar(100), quantidade double, validade date, id_fk int, out msg varchar(100))
BEGIN
DECLARE VerificadorEstoque INT;
IF((lote <> '') AND (quantidade <> '') AND (validade <> '') AND (id_fk <> '')) THEN
	SELECT COUNT(id_est) INTO VerificadorEstoque FROM Estoque WHERE lote = lote_est;
    IF(VerificadorEstoque = 0) THEN
		IF(validade > curdate()) THEN
			INSERT INTO Estoque VALUES(null, lote, quantidade, validade, 'Sim', id_fk);
			SET msg = 'Estoque cadastrado com sucesso!';
        ELSE
			SET msg = 'A data de validade deve ser após hoje!';
        END IF;
    ELSE
		SET msg = 'O lote já existe no sistema!';
    END IF;
ELSE
	SET msg = 'Todos os campos devem estar preenchidos!';
END IF;
END;
$$ DELIMITER ;

#CALL InsertEstoque('20230916CocaCola', 15, '2024-10-12', 1, @ResultEstoque1);
#CALL InsertEstoque('20430927CocaCola', 20, '2026-10-12', 1, @ResultEstoque2);
#CALL InsertEstoque('20130510KitKat', 15, '2024-08-25', 3, @ResultEstoque3);
#SELECT @ResultEstoque1;
#SELECT @ResultEstoque2;
#SELECT @ResultEstoque3;

#Procedimento - Despesa
DELIMITER $$
CREATE PROCEDURE InsertDespesa(nome varchar(100), parcelas int, valor double, dataDesp date, vencimento date , statusDesp varchar(100), id_fk int, out msg varchar(100))
BEGIN
IF((nome <> '') AND (parcelas <> '') AND (valor <> '') AND (dataDesp <> '') AND (vencimento <> '') AND (statusDesp <> '') AND (id_fk <> '')) THEN
	IF(vencimento > curdate()) THEN
		INSERT INTO Despesa VALUES(null, nome, parcelas, valor, dataDesp, vencimento, statusDesp, 'Sim', id_fk);
		SET msg = 'Despesa cadastrada com sucesso!';
	ELSE
		SET msg = 'A data de validade deve ser após hoje!';
	END IF;
ELSE
	SET msg = 'Todos os campos devem estar preenchidos!';
END IF;
END;
$$ DELIMITER ;

#CALL InsertDespesa('Energia', 3, 750, '2023-09-15', '2023-11-15', 'Aberta', 1, @ResultDespesa1);
#CALL InsertDespesa('Água', 2, 350, '2023-08-22', '2023-10-22', 'Aberta', 1, @ResultDespesa2);
#CALL InsertDespesa('Reposição de Estoque', 800, 5, '2023-09-05', '2023-12-05', 'Aberta', 1, @ResultDespesa3);
#SELECT @ResultDespesa1;
#SELECT @ResultDespesa2;
#SELECT @ResultDespesa3;

#Procedimento - Venda
DELIMITER $$
CREATE PROCEDURE InsertVenda(valor double, id_fk int, out msg varchar(100))
BEGIN
DECLARE verificador_fk INT;
DECLARE dataAutal DATE;
DECLARE horaAtual TIME;
IF((valor <> '') AND (id_fk <> '')) THEN
	SELECT COUNT(id_func) into verificador_fk from funcionario where id_func = id_fk;
	IF(Verificador_fk = 1) THEN
		INSERT INTO Venda (id_vend, visivel_vend, data_vend, hora_vend, valor_vend, id_func_fk) VALUES (null, 'Sim', curdate(), curtime(), valor, id_fk);
		SET msg = 'Venda realizada com sucesso!';
	ELSE
		SET msg = 'O funcionário informado não existe no sistema!';
	END IF;
ELSE
    SET msg = 'Todos os campos devem ser preenchidos';
END IF;
END;
$$ DELIMITER ;

#Procedimento - Venda Produto
DELIMITER $$
CREATE PROCEDURE InsertVendaProduto(quantidade int, id_fk int, out msg varchar(100))
BEGIN
DECLARE verificador_fk INT;
DECLARE idVenda int;
IF((quantidade <> '') AND (id_fk <> '')) THEN
	SELECT MAX(id_vend) into idVenda from Venda;
	INSERT INTO Venda_produto (id_vend_prod, visivel_vend_prod, id_prod_fk, id_vend_fk , quantidade_vend_prod) VALUES (null, 'Sim', id_fk, idVenda, quantidade);
	#SET msg = 'Venda realizada com sucesso!';
ELSE
    SET msg = 'Todos os campos devem ser preenchidos';
END IF;
END;
$$ DELIMITER ;

#Procedimento - Primeiro Usuário
DELIMITER $$
CREATE PROCEDURE InsertPrimeiroUsuario(usuario varchar(100), senha varchar(100), out msg varchar(100))
BEGIN
DECLARE verificador_usu INT;
IF((usuario <> '') AND (senha <> '')) THEN
	INSERT INTO funcao (id_fun, visivel_fun, nome_fun, nivel_acess_fun) VALUES (null, 'Não', 'Adm', 4);
	INSERT INTO funcionario (id_func, visivel_func, nome_func, id_fun_fk, nivel_acess_func) VALUES (null, 'Não', 'Adm', 1, 4);
	INSERT INTO usuario (id_usu, visivel_usu, usuario_usu, senha_usu, nivel_acess_usu, id_func_fk) VALUES (null, 'Sim', usuario, senha, 4, 1);
    SET msg = 'Usuário cadastrado com sucesso!';
ELSE
    SET msg = 'Todos os campos devem ser preenchidos';
END IF;
END;
$$ DELIMITER ;

#CALL InsertPrimeiroUsuario('Adm', '123', @ResultPmUser);
#SELECT @ResultPmUser;

#Procedimento - Usuário
DELIMITER $$
CREATE PROCEDURE InsertUsuario(usuario varchar(100), senha varchar(100), id_fk int, acesso int, out msg varchar(100))
BEGIN
DECLARE verificador_usu INT;
DECLARE verificador_func INT;
IF((usuario <> '') AND (senha <> '') AND (id_fk <> '')) THEN
	SELECT count(id_usu) INTO verificador_usu FROM usuario WHERE usuario_usu = usuario;
    IF(verificador_usu = 0) THEN
        SELECT count(id_usu) INTO verificador_func FROM usuario WHERE id_func_fk = id_fk;
        IF(verificador_func = 0) THEN
			INSERT INTO usuario (id_usu, visivel_usu, usuario_usu, senha_usu, id_func_fk, nivel_acess_usu) VALUES (null, 'Sim', usuario, senha, id_fk, acesso);
			SET msg = 'Usuário cadastrado com sucesso!';
        ELSE
			SET msg = 'O funcionário informado já possui um login no sistema!';
        END IF;
    ELSE
        SET msg = 'O usuário informado já existe no sistema!';
    END IF;
ELSE
    SET msg = 'Todos os campos devem ser preenchidos';
END IF;
END;
$$ DELIMITER ;

#CALL InsertPrimeiroUsuario('Welliton', '123', 2, 1, @ResultUser1);
#CALL InsertPrimeiroUsuario('Giovana', '123', 3, 1, @ResultUser2);
#CALL InsertPrimeiroUsuario('Milene', '123', 4, 1, @ResultUser3);
#SELECT @ResultUser1;
#SELECT @ResultUser1;
#SELECT @ResultUser1;

#Checks
#select * from Usuario;
#select * from Cidade;
#select * from Caixa;
#select * from Cliente;
#select * from Funcionario;
#select * from Fornecedor;
#select * from Funcao;
#select * from Produto;
#select * from Pagamento;