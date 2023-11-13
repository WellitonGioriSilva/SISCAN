#Ana Carolina do Vale
#Giovana Oliveira Demuner
#Lara Beatriz de Abreu
#Milene Silveira Kruguel
#Welliton Giori Silva

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
cidade_forn varchar(100),
estado_forn varchar(100)
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
telefone_cli varchar(30),
cidade_cli varchar(100),
estado_cli varchar(100)
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
cidade_func varchar(100),
estado_func varchar(100),
id_fun_fk int,
foreign key (id_fun_fk) references Funcao (id_fun)
);

create table Caixa
(
id_cai int primary key auto_increment,
data_cai date,
hora_abertura_cai time,
hora_fechamento_cai time,
valor_inicial_cai double,
valor_final_cai double,
visivel_cai varchar(10),
status_cai varchar(10),
id_func_fk int,
foreign key(id_func_fk) references Funcionario(id_func)
);

create table Venda
(
id_vend int primary key auto_increment,
data_vend date,
hora_vend time,
valor_vend double,
visivel_vend varchar(10),
id_func_fk int,
foreign key (id_func_fk) references Funcionario (id_func),
id_cli_fk int,
foreign key (id_cli_fk) references Cliente (id_cli),
id_cai_fk int,
foreign key (id_cai_fk) references Caixa (id_cai) 
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
valor_parcela_desp double,
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

##########################################	Procedimentos	##########################################

#Procedimento - Produto
DELIMITER $$
CREATE PROCEDURE InsertProduto(nome varchar(100), marca varchar(100), tipo varchar(100), valorCom double)
BEGIN
DECLARE VerificadorProduto int;
DECLARE ValorVen double;
DECLARE Produto_Fk int;
IF ((nome <> '') AND (marca <> '') AND (tipo <> '') AND (valorCom <> '')) THEN
	SELECT COUNT(id_prod) into VerificadorProduto from produto where nome = nome_prod;
    IF(VerificadorProduto = 0) THEN 
		SET valorVen = valorCom + ((valorCom * 60) / 100);
		INSERT INTO Produto VALUES (null, nome, marca, tipo, valorCom, valorVen, 'Sim');
        SELECT "Produto salvo com sucesso!", true as resultado;
    ELSE
        SELECT "O produto já existe no sitema!", false as resultado;
	END IF;
ELSE
    SELECT "Todos os campos devem estar preenchidos!", false as resultado;
END IF; 
END;
$$ DELIMITER ;

#Procedimento - Caixa
DELIMITER $$
CREATE PROCEDURE InsertCaixa(valorIni double, id_func int)
BEGIN
DECLARE dataAtual date;
INSERT INTO caixa (id_cai, data_cai, hora_abertura_cai, valor_inicial_cai, visivel_cai, status_cai, id_func_fk) VALUES(null, curdate(), curtime(), valorIni, 'Sim', 'Aberto', id_func);
SELECT "Caixa salvo com sucesso!", true as resultado;
END;
$$ DELIMITER ;

#Procedimento - Função
DELIMITER $$
CREATE PROCEDURE InsertFuncao(nome varchar(100), salario double, turno varchar(100), nivel_acess varchar(100))
BEGIN
DECLARE VerificadorFuncao INT;
IF((nome <> '') AND (salario <> '') AND (turno <> '') AND (nivel_acess <> '')) THEN
	SELECT COUNT(id_fun) INTO VerificadorFuncao FROM Funcao WHERE nome = nome_fun;
    IF(VerificadorFuncao = 0) THEN
		INSERT INTO Funcao VALUES(null, nome, salario, turno, nivel_acess, 'Sim');
        SELECT "Função salva com sucesso!", true as resultado;
    ELSE
        SELECT "A função já existe no sistema!", false as resultado;
    END IF;
ELSE
    SELECT "Todos os campos devem estar preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Cliente
DELIMITER $$ 
CREATE PROCEDURE InsertCliente(nome_cli varchar(45), cpf varchar(45), email_cli varchar(45), sexo_cli varchar(45), data_nascimento_cli date, rua_cli varchar(45), bairro_cli varchar(45),
numero_cli int, cidade varchar(100), estado varchar(100))
BEGIN
DECLARE cpf_existe INT;
SELECT COUNT(*) INTO cpf_existe FROM Cliente WHERE cpf_cli = cpf;
IF (cpf_existe = 0) THEN
	SELECT "Cliente cadastrado com sucesso!", true as SUCESSO;
	INSERT INTO Cliente (nome_cli, cpf_cli, email_cli, sexo_cli, data_nascimento_cli, rua_cli, bairro_cli, numero_cli, visivel_cli, cidade_cli, estado_cli) VALUES (nome_cli, cpf, email_cli, sexo_cli, data_nascimento_cli, rua_cli, bairro_cli, numero_cli, 'Sim', cidade, estado);
ELSE
	SELECT "CPF já cadastrado!", false as ERRO;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Fornecedor
DELIMITER $$
CREATE PROCEDURE InsertFornecedor(razao_social_forn varchar(45), cnpj_forn varchar(45), bairro_forn varchar(45), rua_forn varchar(45), nome_fantasia_forn varchar(45), 
telefone_forn varchar(45), inscricao_estadual_forn varchar(45),  responsavel_forn varchar(45), cidade varchar(100), estado varchar(100))
BEGIN
    DECLARE cnpj_existe INT;
    DECLARE telefone_existe INT;
    SELECT COUNT(*) INTO cnpj_existe FROM Fornecedor WHERE cnpj_forn = cnpj_forn;
    IF cnpj_existe > 0 THEN
        SELECT "CNPJ já cadastrado!", false as resultado;
	ELSE
		SELECT COUNT(*) INTO telefone_existe FROM Fornecedor WHERE telefone_forn = telefone_forn;
		IF telefone_existe > 0 THEN
            SELECT "Telefone já cadastrado!", false as resultado;
		ELSE
			INSERT INTO Fornecedor (razao_social_forn, cnpj_forn, bairro_forn, rua_forn, nome_fantasia_forn, telefone_forn, inscricao_estadual_forn,
			responsavel_forn, visivel_forn, cidade_forn, estado_forn) VALUES (razao_social_forn, cnpj_forn, bairro_forn, rua_forn, nome_fantasia_forn, telefone_forn, inscricao_estadual_forn, responsavel_forn, 'Sim', cidade, estado);
            SELECT "Fornecedor cadastrado com sucesso!", true as resultado;
		END IF;
    END IF;
END
$$ DELIMITER ;

#Procedimento - Funcionario
delimiter $$
create procedure InsertFuncionario(nome varchar(100), bairro varchar(100), rua varchar(100),
cpf varchar(50), numero int, sexo varchar(50), cidade varchar(100), estado varchar(100), funcao_fk int, nivelAcesso int)
begin
declare cpfteste varchar(50);
set cpfteste = (select cpf_func from Funcionario where (cpf_func = cpf));
if(cpfteste = '') or (cpfteste is null) then
	insert into Funcionario values(null, nome, bairro, rua, cpf, numero, sexo, nivelAcesso,'Sim', cidade, estado, funcao_fk);
    SELECT "Funcionário cadastrado com sucesso!", true as resultado;
else
    SELECT "O cpf informado já cadastrado!", false as resultado;
end if;
end;
$$ delimiter ;

#Procedimento - Compra
DELIMITER $$
CREATE PROCEDURE InsertCompra(valor double, id_fk int, dataVencimento date, statusDespesa varchar(10), parcelas int)
BEGIN
DECLARE Verificador_fk int;
DECLARE idCompra int;
DECLARE nomeDespesa varchar(100);
DECLARE valorParcela double;
IF ((valor <> '') AND (id_fk <> '')) THEN
	SELECT COUNT(id_forn) into Verificador_fk from fornecedor where id_forn = id_fk;
	IF(Verificador_fk = 1) THEN
		INSERT INTO Compra VALUES(null, valor, curdate(), 'Sim', id_fk);
        SET nomeDespesa = concat(curdate(),' Reposição de Estoque');
        SELECT MAX(id_com) into idCompra from Compra;
        SET valorParcela = (valor / parcelas);
        INSERT INTO Despesa VALUES(null, nomeDespesa, parcelas, valor, valorParcela, curdate(), dataVencimento, statusDespesa, 'Sim', idCompra);
        SELECT "Compra salva com sucesso!", true as SUCESSO;
	ELSE
        SELECT "O fornecedor informado não existe no sistema!", false as resultado;
	END IF;
ELSE
    SELECT "Todos os campos devem estar preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Compra Produto
DELIMITER $$
CREATE PROCEDURE InsertCompraProduto(lote varchar(100), quantidade int, id_fk int, validade date)
BEGIN
DECLARE verificador_fk INT;
DECLARE idCompra int;
DECLARE verifcEstoque int;
IF((quantidade <> '') AND (id_fk <> '')) THEN
	SELECT MAX(id_com) into idCompra from Compra;
	INSERT INTO Compra_produto VALUES(null, quantidade, 'Sim', idCompra, id_fk);
    SELECT count(id_est) INTO verifcEstoque FROM Estoque WHERE lote_est = lote;
    IF(verifcEstoque = 0) THEN
		INSERT INTO Estoque VALUES(null, lote, quantidade, validade, 'Sim', id_fk);
	ELSE
		UPDATE Estoque SET quantidade_est = quantidade_est + quantidade, validade_est = validade, visivel_est = "Sim";
	END IF;
ELSE
    SELECT "Todos os campos devem ser preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Venda
DELIMITER $$
CREATE PROCEDURE InsertVenda(valor double, id_fk int, idCaixa int, idFormaPag int, idCliente int)
BEGIN
DECLARE verificador_fk INT;
DECLARE idVenda INT;
IF((valor <> '') AND (id_fk <> '')) THEN
	SELECT COUNT(id_func) into verificador_fk from funcionario where id_func = id_fk;
	IF(Verificador_fk = 1) THEN
		INSERT INTO Venda (id_vend, visivel_vend, data_vend, hora_vend, valor_vend, id_func_fk, id_cli_fk, id_cai_fk) VALUES (null, 'Sim', curdate(), curtime(), valor, id_fk, idCliente, idCaixa);
        SET idVenda = (SELECT max(id_vend) FROM venda);
        INSERT INTO Recebimento VALUES(null, curdate(), valor, curtime(), 'Sim', idVenda, idCaixa, idFormaPag);
        SELECT "Venda realizada com sucesso!", true as SUCESSO;
	ELSE
        SELECT "O funcionário informado não existe no sistema!", false as resultado;
	END IF;
ELSE
    SELECT "Todos os campos devem ser preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Venda Produto
DELIMITER $$
CREATE PROCEDURE InsertVendaProduto(quantidade int, id_fk int)
BEGIN
DECLARE verificador_fk INT;
DECLARE idVenda int;
DECLARE idEstoque int;
DECLARE verQuantidade int;
DECLARE nomeProd varchar(100);
IF((quantidade <> '') AND (id_fk <> '')) THEN
	SELECT MAX(id_vend) into idVenda from Venda;
    SELECT id_est INTO idEstoque FROM Estoque WHERE (id_prod_fk = id_fk);
    SELECT quantidade_est INTO verQuantidade FROM Estoque WHERE (id_prod_fk = id_fk);
    SELECT nome_prod INTO nomeProd FROM Produto WHERE (id_prod = id_fk);
    IF(verQuantidade >= quantidade) THEN
		UPDATE Estoque SET quantidade_est = quantidade_est - quantidade WHERE id_est = idEstoque;
		INSERT INTO Venda_produto (id_vend_prod, visivel_vend_prod, id_prod_fk, id_vend_fk , quantidade_vend_prod) VALUES (null, 'Sim', id_fk, idVenda, quantidade);
		SELECT "0", true as SUCESSO;
    ELSE
		SELECT concat('Só existem ', verQuantidade, ' ', nomeProd, ' no estoque!'), true as SUCESSO;
        UPDATE Estoque SET quantidade_est = quantidade_est - verQuantidade WHERE id_est = idEstoque;
		INSERT INTO Venda_produto (id_vend_prod, visivel_vend_prod, id_prod_fk, id_vend_fk , quantidade_vend_prod) VALUES (null, 'Sim', id_fk, idVenda, quantidade);
    END IF;
ELSE
    SELECT "Todos os campos devem ser preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Despesa
DELIMITER $$
CREATE PROCEDURE InsertDespesa(nome varchar(100), parcelas int, valor double, dataDesp date, vencimento date , statusDesp varchar(100), id_fk int)
BEGIN
DECLARE valor_parcela double;
IF((nome <> '') AND (parcelas <> '') AND (valor <> '') AND (statusDesp <> '') AND (id_fk <> '')) THEN
	IF(vencimento > curdate()) THEN
		SET valor_parcela = (valor / parcelas);
		INSERT INTO Despesa VALUES(null, nome, parcelas, valor, valor_parcela, dataDesp, vencimento, statusDesp, 'Sim', id_fk);
        SELECT "Despesa cadastrada com sucesso!", true as SUCESSO;
	ELSE
        SELECT "A data de validade deve ser após hoje!", false as resultado;
	END IF;
ELSE
    SELECT "Todos os campos devem estar preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Pagamento
DELIMITER $$
CREATE PROCEDURE InsertPagamento(valor double, id_caixa int, id_despesa int, id_form int, parcela int, dataNova date)
BEGIN
DECLARE verificador_fk int;
DECLARE data_pag date;
DECLARE hora_pag time;
DECLARE verificacaoParcelas int;

IF((valor <>'') AND (id_despesa <> '')) THEN 
	SELECT COUNT(id_desp) into verificador_fk from Despesa where id_desp = id_despesa;
	IF(verificador_fk = 1) THEN 
        INSERT INTO Pagamento(id_pag, data_pag, visivel_pag, hora_pag, valor_pag, id_desp_fk, id_cai_fk, id_form_pag_fk) values (null, curdate(), 'Sim', curtime(), valor, id_despesa, id_caixa, id_form);
        SELECT (parcelas_desp - parcela) INTO verificacaoParcelas FROM Despesa;
        IF(verificacaoParcelas = 0) THEN
			UPDATE Despesa SET parcelas_desp = parcelas_desp - parcela, vencimento_desp = dataNova, status_desp = 'Fechada' WHERE id_desp = id_despesa;
		ELSE
			UPDATE Despesa SET parcelas_desp = parcelas_desp - parcela, vencimento_desp = dataNova WHERE id_desp = id_despesa;
		END IF;
        SELECT "Pagamento realizado com sucesso!", true as resultado;
	ELSE 
		SELECT "A despesa informada não existe no sistema!", false as resultado;
	END IF;
ELSE
	SELECT "Todos os campos devem ser preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ; 

#Procedimento - Recebimento
DELIMITER $$
CREATE PROCEDURE InsertRecebimento(valor double, data_rec date, id_fk int)
BEGIN
DECLARE verificador_fk int;
DECLARE data_rec date;
DECLARE hora_rec time;

IF((valor <>'') AND (id_fk <> '')) THEN 
	SELECT COUNT(id_vend) into verificador_fk from Venda where id_vend = id_fk;
	IF(verificador_fk = 1) THEN 
        INSERT INTO Recebimento(id_rec, visivel_rec, hora_rec, valor_rec,  id_vend_fk) values (null, 'Sim', curdate(), curtime(), valor, id_fk);
        SELECT "A Venda não existe no sistema!", true as resultado;
	ELSE 
        SELECT "A Venda não existe no sistema!", false as resultado;
	END IF;
ELSE
    SELECT "Todos os campos devem ser preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Primeiro Usuário
DELIMITER $$
CREATE PROCEDURE InsertPrimeiroUsuario(usuario varchar(100), senha varchar(100))
BEGIN
DECLARE verificador_usu INT;
IF((usuario <> '') AND (senha <> '')) THEN
	INSERT INTO funcao (id_fun, visivel_fun, nome_fun, nivel_acess_fun) VALUES (null, 'Não', 'Adm', 4);
	INSERT INTO funcionario (id_func, visivel_func, nome_func, id_fun_fk, nivel_acess_func) VALUES (null, 'Não', 'Adm', 1, 4);
	INSERT INTO usuario (id_usu, visivel_usu, usuario_usu, senha_usu, nivel_acess_usu, id_func_fk) VALUES (null, 'Sim', usuario, senha, 4, 1);
    SELECT "Usuário cadastrado com sucesso!", true as SUCESSO;
ELSE
    SELECT "Todos os campos devem ser preenchidos!", false as ERRO;
END IF;
END;
$$ DELIMITER ;

#Procedimento - Usuário
DELIMITER $$
CREATE PROCEDURE InsertUsuario(usuario varchar(100), senha varchar(100), id_fk int, acesso int)
BEGIN
DECLARE verificador_usu INT;
DECLARE verificador_func INT;
IF((usuario <> '') AND (senha <> '') AND (id_fk <> '')) THEN
	SELECT count(id_usu) INTO verificador_usu FROM usuario WHERE usuario_usu = usuario;
    IF(verificador_usu = 0) THEN
        SELECT count(id_usu) INTO verificador_func FROM usuario WHERE id_func_fk = id_fk;
        IF(verificador_func = 0) THEN
			INSERT INTO usuario (id_usu, visivel_usu, usuario_usu, senha_usu, id_func_fk, nivel_acess_usu) VALUES (null, 'Sim', usuario, senha, id_fk, acesso);
             SELECT "Usuário cadastrado com sucesso!", true as SUCESSO;
        ELSE
            SELECT "O funcionário informado já possui um login no sistema!", false as resultado;
        END IF;
    ELSE
        SELECT "O usuário informado já existe no sistema!", false as resultado;
    END IF;
ELSE
    SELECT "Todos os campos devem ser preenchidos!", false as resultado;
END IF;
END;
$$ DELIMITER ;

#Procedimento - GetByIdCaixa
DELIMITER $$
CREATE PROCEDURE GetByIdCaixa(out valor double, out id int)
BEGIN
	SELECT id_cai INTO id FROM Caixa WHERE (status_cai = "Aberto");
	SELECT valor_inicial_cai INTO valor FROM Caixa WHERE (status_cai = "Aberto");
END;
$$ DELIMITER ;

#Procedimento - Fechamento Caixa
DELIMITER $$
CREATE PROCEDURE FechamentoCaixa(id int, valor_final double)
BEGIN
	UPDATE Caixa SET hora_fechamento_cai = curtime(), valor_final_cai = valor_final, status_cai = "Fechado" WHERE id_cai = id;
	SELECT "Caixa fechado com sucesso!", true as resultado;
END;
$$ DELIMITER ;

#Procedimento - Valor Total Pagamento
DELIMITER $$
CREATE PROCEDURE ValorTotalPagamento(id int, out valor_final double)
BEGIN
	SELECT SUM(valor_pag) INTO valor_final FROM Pagamento WHERE (id_cai_fk = id);
END;
$$ DELIMITER ;

#Procedimento - Valor Total Recebimento
DELIMITER $$
CREATE PROCEDURE ValorTotalRecebimento(id int, out valor_final double)
BEGIN
	SELECT SUM(valor_rec) INTO valor_final FROM Recebimento WHERE (id_cai_fk = id);
END;
$$ DELIMITER ;

#Checks	
SELECT * FROM venda;
SELECT * FROM venda_produto;
SELECT * FROM caixa;

#Extrato
DELIMITER $$
CREATE PROCEDURE ExtratoCaixa(id int)
BEGIN
	SELECT
    Caixa.id_cai as 'Número do Caixa',
    Caixa.data_cai as 'Data',
    Caixa.hora_abertura_cai as 'Hora de Abertura',
    Caixa.hora_fechamento_cai as 'Hora de Fechamento',
    Caixa.valor_inicial_cai as 'Valor Inicial',
    Caixa.valor_final_cai as 'Valor Final',
    Funcionario.nome_func as 'Funcionário Responsável'
    FROM Caixa, Funcionario, Venda
    WHERE 
    ((Caixa.id_cai = id) AND (Caixa.visivel_cai = 'Sim')) AND (Venda.id_cai_fk = id) AND ((Venda.id_func_fk = Funcionario.id_func))
    GROUP BY 
    Caixa.id_cai,
	Caixa.data_cai,
	Caixa.hora_abertura_cai,
	Caixa.hora_fechamento_cai,
	Caixa.valor_inicial_cai,
	Caixa.valor_final_cai,
	Funcionario.nome_func
    ORDER BY Caixa.id_cai;
END;
$$ DELIMITER ;

DELIMITER $$
CREATE PROCEDURE ExtratoVendas(id int)
BEGIN
	SELECT
    Venda.id_vend as 'Número da Venda',
    Venda.data_vend as 'Data',
    Venda.hora_vend as 'Hora da venda',
    Produto.nome_prod as 'Produtos',
    Venda_produto.quantidade_vend_prod as 'Quantidade de Produtos',
    Venda.valor_vend as 'Valor da venda'
    FROM Venda, Produto, Venda_produto
    WHERE 
    ((Venda_produto.id_vend_fk = Venda.id_vend) AND (Produto.id_prod = Venda_produto.id_prod_fk)) AND (Venda.id_cai_fk = id)
    GROUP BY 
    Venda.id_vend,
    Venda.data_vend,
    Venda.hora_vend,
    Produto.nome_prod,
	Venda_produto.quantidade_vend_prod,
    Venda.valor_vend
    ORDER BY Venda.id_vend;
END;
$$ DELIMITER ;

DELIMITER $$
CREATE PROCEDURE Lembrete()
BEGIN
DECLARE maxVencimento date;
SELECT max(vencimento_desp) INTO maxVencimento FROM Despesa WHERE (visivel_desp = "Sim");
SELECT * FROM Despesa WHERE vencimento_desp = maxVencimento;
END;
$$ DELIMITER ;

CALL ExtratoVendas(1);

select * from fornecedor;
select * from compra_produto;