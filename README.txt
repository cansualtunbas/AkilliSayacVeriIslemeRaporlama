

Proje mikroservis mimarisine uygun olarak hazýrlandý.

Katmanlar:
 -IdentityServer : identityserver4 OAuth 2.0 (Authorization) ve OpenId Connect (Authentication) protokollerini implement eden bir framework olduðu için üyelik sistemi için kullanýldý. Mikroservisler (Counter-Report) identityserver ile (resourceownerpassword) koruma altýna alýnarak eriþim token'lar ile saðlandý. Identityserver projeye implement edilirken hazýr template yapýsý ile implement edildi fakat sunulan hazýr ön yüz template kullanýlmadý sadece endpointlerinin OAuth ve OpenId protokollerine uygun olmasý sebebiyle tercih edildi. Proje ayaða kalktýðýnda database hazýr dummy bir kullanýcý oluþturulmasý için scope içinde dummy kullanýcý bilgileri eklendi.
	NOT:Identityserver projesinde diðer versiyonlarý ücretli olduðu için netcoreapp3.1 kullanýldý.

 -Shared : Bu katman tüm projelerde ortak olacak methodlarýn kullanýmýna uygun olarak hazýrlandý. Ayrýca mesaj kuyruk sistemi için kullanýlan rabbitmq'nun event ve commandlarýnýn ayný namespace altýnda olmasý gerektiðinden Messages klasörü altýnda bu eventler oluþturuldu.

 -Services :
	-Counter Servisi: Database olarak Mongo DB kullanýlmýþtýr. Bu api projesinde sayaç ekleme, sayaç güncelleme ve sayaç silme iþlemlerinin yapýlabileceði ortam hazýrlanmýþtýr. Proje ayaða kalktýðýnda database hazýr dummy sayaçlar oluþturulmasý için scope içinde dummy sayaçlar eklendi.
	-Report Servisi:  Database olarak Mongo DB kullanýlmýþtýr. Rapor durumundaki deðiþikliðin saðlanmasý amacýyla Consumer klasörü oluþturuldu. Bu projede sayaç numaralarýna göre sýralama yapýlarak rapor çýktýsýnýn excel olarak alýnmasý saðlandý. Rapor oluþtur dendiðinde rapor tablosuna rapor durumu 0 (hazýrlanýyor) olan kayýt oluþturulduktan sonra mesaj kuyruk sistemi ile durumu 1(Tamamlandý) set ediliyor.

 -Gateways : Gelen isteklerin mikroservislerin port numarasýyla ilgilenilmeden tek bir port üzerinden haberleþmesi amacýyla bu proje oluþturuldu. Ocelot kütüphanesi kullanýldý ve tabii ki identity server ile koruma altýna alýndý.

  -Web : Web projesi mvc olarak hazýrlandý ve sayfalar için boostrapt template kullanýldý.


  Projede yer alan tüm databaseler dockerize edildi ve proje ayaða kaldýrýlmadan önce aþaðýda bahsedilen þekilde docker dosyalarýnýn ayaða kaldýrýlmasý gerekmektedir.

  Adým 1: proje klasörü içinde MicroserviceDatabases adýndaki klasörünün uzantýsýna powershell içinde gittikten sonra 
  (örn:
  cd C:\Users\Cansu\source\Repos\AkilliSayac\AkilliSayacVeriIslemeRaporlama\MicroserviceDatabases)

  docker-compose up

  komutunu çalýþtýrmanýz gerekmektedir. Eðer docker desktop kurulu ise rabbitmqcontainer,identitydb,AkilliSayacDb container'larýný görebilirsiniz. Container'lar ayaða kalktýktan sonra localinizde  Shared projesi hariç diðer tüm projeleri multiple seçmeniz ve çalýþtýrmanýz gerekmektedir. Ardýndan tarayýcýnýzdan http://localhost:5010/ adresine gidebilirsiniz. Tüm port bilgileri Port.txt dosyasýnda yer almaktadýr.

  Teþekkürler,

  Cansu altunbaþ
