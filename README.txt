

Proje mikroservis mimarisine uygun olarak haz�rland�.

Katmanlar:
 -IdentityServer : identityserver4 OAuth 2.0 (Authorization) ve OpenId Connect (Authentication) protokollerini implement eden bir framework oldu�u i�in �yelik sistemi i�in kullan�ld�. Mikroservisler (Counter-Report) identityserver ile (resourceownerpassword) koruma alt�na al�narak eri�im token'lar ile sa�land�. Identityserver projeye implement edilirken haz�r template yap�s� ile implement edildi fakat sunulan haz�r �n y�z template kullan�lmad� sadece endpointlerinin OAuth ve OpenId protokollerine uygun olmas� sebebiyle tercih edildi. Proje aya�a kalkt���nda database haz�r dummy bir kullan�c� olu�turulmas� i�in scope i�inde dummy kullan�c� bilgileri eklendi.
	NOT:Identityserver projesinde di�er versiyonlar� �cretli oldu�u i�in netcoreapp3.1 kullan�ld�.

 -Shared : Bu katman t�m projelerde ortak olacak methodlar�n kullan�m�na uygun olarak haz�rland�. Ayr�ca mesaj kuyruk sistemi i�in kullan�lan rabbitmq'nun event ve commandlar�n�n ayn� namespace alt�nda olmas� gerekti�inden Messages klas�r� alt�nda bu eventler olu�turuldu.

 -Services :
	-Counter Servisi: Database olarak Mongo DB kullan�lm��t�r. Bu api projesinde saya� ekleme, saya� g�ncelleme ve saya� silme i�lemlerinin yap�labilece�i ortam haz�rlanm��t�r. Proje aya�a kalkt���nda database haz�r dummy saya�lar olu�turulmas� i�in scope i�inde dummy saya�lar eklendi.
	-Report Servisi:  Database olarak Mongo DB kullan�lm��t�r. Rapor durumundaki de�i�ikli�in sa�lanmas� amac�yla Consumer klas�r� olu�turuldu. Bu projede saya� numaralar�na g�re s�ralama yap�larak rapor ��kt�s�n�n excel olarak al�nmas� sa�land�. Rapor olu�tur dendi�inde rapor tablosuna rapor durumu 0 (haz�rlan�yor) olan kay�t olu�turulduktan sonra mesaj kuyruk sistemi ile durumu 1(Tamamland�) set ediliyor.

 -Gateways : Gelen isteklerin mikroservislerin port numaras�yla ilgilenilmeden tek bir port �zerinden haberle�mesi amac�yla bu proje olu�turuldu. Ocelot k�t�phanesi kullan�ld� ve tabii ki identity server ile koruma alt�na al�nd�.

  -Web : Web projesi mvc olarak haz�rland� ve sayfalar i�in boostrapt template kullan�ld�.


  Projede yer alan t�m databaseler dockerize edildi ve proje aya�a kald�r�lmadan �nce a�a��da bahsedilen �ekilde docker dosyalar�n�n aya�a kald�r�lmas� gerekmektedir.

  Ad�m 1: proje klas�r� i�inde MicroserviceDatabases ad�ndaki klas�r�n�n uzant�s�na powershell i�inde gittikten sonra 
  (�rn:
  cd C:\Users\Cansu\source\Repos\AkilliSayac\AkilliSayacVeriIslemeRaporlama\MicroserviceDatabases)

  docker-compose up

  komutunu �al��t�rman�z gerekmektedir. E�er docker desktop kurulu ise rabbitmqcontainer,identitydb,AkilliSayacDb container'lar�n� g�rebilirsiniz. Container'lar aya�a kalkt�ktan sonra localinizde  Shared projesi hari� di�er t�m projeleri multiple se�meniz ve �al��t�rman�z gerekmektedir. Ard�ndan taray�c�n�zdan http://localhost:5010/ adresine gidebilirsiniz. T�m port bilgileri Port.txt dosyas�nda yer almaktad�r.

  Te�ekk�rler,

  Cansu altunba�
