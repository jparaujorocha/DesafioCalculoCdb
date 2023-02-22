# DesafioCalculoCdb

O backend está realizando suas operações normalmente, podendo ser feita as requisições via postman por exemplo, enquanto o front-end ainda apresenta
 alguns problemas não resolvidos e padrões não tão bem aplicados por já estar atrasado com a entrega.

Foi utilizado o banco de dados SqlServer versão 19, localmente. Para o correto funcionamento do sistema,, apagar o conteúdo da pasta "DesafioCalculoCdb.Infra.Data.Migrations", ir até o package console, 
apontar o campo projeto padrão do prompt para DesafioCalculoCdb.Infra.Data e o projeto PADRÃO DE INICIALIZAÇÃO para o DesafioCalculoCdb.Infra.Api e realizar os comandos abaixo:

"add-migration DesafioCalculo"

"update-database"

Após isso o projeto estará pronto para ser rodado localmente, podendo ser alterado seu apontamento pelo appsettings presente no projeto Api
