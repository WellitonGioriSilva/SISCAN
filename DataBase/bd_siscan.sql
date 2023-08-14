create database bd_siscan;
use bd_siscan;

create table Produto
(
id_prod int primary key auto_increment,
nome_prod varchar(45),
marca_prod varchar(45),
tipo_prod varchar(45),
foto_prod varchar(45)
);

create table Caixa
(
id_cai int primary key auto_increment,
data_cai date,
hora_abertura_cai time,
hora_fechamento_cai time,
valor_inicial_cai double,
valor_final_cai double
);

create table Funcao(
id_fun int primary key auto_increment,
nome_fun varchar(45),
salario_fun double,
turno_fun varchar(45)
);

create table Forma_Pagamento
(
id_form_pag int primary key auto_increment,
nome_form_pag varchar(45)
);

create table Estado
(
 id_est int primary key auto_increment,
 nome_est varchar(45),
 sigla_est varchar(45),
 uf_est int
);

create table Cidade
(
id_cid int primary key auto_increment,
nome_cid varchar(45),
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
status_vend varchar(45),
peso_vend double,
id_cid_fk int,
foreign key (id_cid_fk) references Cidade (id_cid),
id_func_fk int,
foreign key (id_func_fk) references Funcionario (id_func) 
);

create table Venda_produto
(
id_vend_prod int primary key auto_increment,
quantidade_vend_prod int,
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
id_prod_fk int,
foreign key (id_prod_fk) references produto (id_prod)
);

create table Compra
(
id_com int primary key auto_increment,
valor_com double,
data_com date,
id_forn_fk int,
foreign key (id_forn_fk) references Fornecedor (id_forn)
);

create table Compra_produto
(
id_com_prod int primary key auto_increment,
quantidade_com_prod int,
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
id_com_fk int,
foreign key (id_com_fk) references Compra (id_com)
);

create table Pagamento
(
id_pag int primary key auto_increment,
data_pag date, 
valor_pag double,
hora_pag time,
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
id_func_fk int,
foreign key (id_func_fk) references Funcionario (id_func) 
);

#Inserts
INSERT INTO Forma_Pagamento VALUES(null, 'Cartão de Crédito'),
(null, 'Cartão de Débito'),
(null, 'Pix');

INSERT INTO Estado VALUES(null, "Rôndonia", "RO", 11);

INSERT INTO Cidade VALUES
(null, 'Alta Floresta d''Oeste', 1),
(null, 'Ariquemes', 1),
(null, 'Cabixi', 1),
(null, 'Cacoal', 1),
(null, 'Cerejeiras', 1),
(null, 'Colorado do Oeste', 1),
(null, 'Corumbiara', 1),
(null, 'Costa Marques', 1),
(null, 'Espigão d''Oeste', 1),
(null, 'Governador Jorge Teixeira', 1),
(null, 'Guajará-Mirim', 1),
(null, 'Jaru', 1),
(null, 'Ji-Paraná', 1),
(null, 'Machadinho d''Oeste', 1),
(null, 'Nova Brasilândia d''Oeste', 1),
(null, 'Ouro Preto do Oeste', 1),
(null, 'Pimenta Bueno', 1),
(null, 'Porto Velho', 1),
(null, 'Presidente Médici', 1),
(null, 'Rio Crespo', 1),
(null, 'Rolim de Moura', 1),
(null, 'Santa Luzia d''Oeste', 1),
(null, 'Vilhena', 1),
(null, 'São Miguel do Guaporé', 1),
(null, 'Nova Mamoré', 1),
(null, 'Alvorada d''Oeste', 1),
(null, 'Alto Alegre dos Parecis', 1),
(null, 'Alto Paraíso', 1),
(null, 'Buritis', 1),
(null, 'Novo Horizonte do Oeste', 1),
(null, 'Cacaulândia', 1),
(null, 'Campo Novo de Rondônia', 1),
(null, 'Candeias do Jamari', 1),
(null, 'Castanheiras', 1),
(null, 'Chupinguaia', 1),
(null, 'Cujubim', 1),
(null, 'Governador Jorge Teixeira', 1),
(null, 'Itapuã do Oeste', 1),
(null, 'Ministro Andreazza', 1),
(null, 'Mirante da Serra', 1),
(null, 'Monte Negro', 1),
(null, 'Nova União', 1),
(null, 'Parecis', 1),
(null, 'Pimenteiras do Oeste', 1),
(null, 'Primavera de Rondônia', 1),
(null, 'São Felipe d''Oeste', 1),
(null, 'São Francisco do Guaporé', 1),
(null, 'Seringueiras', 1),
(null, 'Teixeirópolis', 1),
(null, 'Theobroma', 1),
(null, 'Urupá', 1),
(null, 'Vale do Anari', 1),
(null, 'Vale do Paraíso', 1);

#Checks
select * from Cidade;
select * from Caixa;
select * from Cliente;
select * from Funcionario;
select * from Fornecedor;
select * from Funcao;
select * from Produto;
select * from Pagamento;
SELECT * FROM Cliente, Cidade WHERE (Cliente.id_cid_fk = Cidade.id_cid) AND (nome_cli LIKE '%Welliton%');