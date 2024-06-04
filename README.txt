

Proje mikroservis mimarisine uygun olarak hazırlandı.

Katmanlar:
 -IdentityServer : identityserver4 OAuth 2.0 (Authorization) ve OpenId Connect (Authentication) protokollerini implement eden bir framework olduğu için üyelik sistemi için kullanıldı. Mikroservisler (Counter-Report) identityserver ile (resourceownerpassword) koruma altına alınarak erişim token'lar ile sağlandı. Identityserver projeye implement edilirken hazır template yapısı ile implement edildi fakat sunulan hazır ön yüz template kullanılmadı sadece endpointlerinin OAuth ve OpenId protokollerine uygun olması sebebiyle tercih edildi. Proje ayağa kalktığında database hazır dummy bir kullanıcı oluşturulması için scope içinde dummy kullanıcı bilgileri eklendi.
	NOT:Identityserver projesinde diğer versiyonları ücretli olduğu için netcoreapp3.1 kullanıldı.

 -Shared : Bu katman tüm projelerde ortak olacak methodların kullanımına uygun olarak hazırlandı. Ayrıca mesaj kuyruk sistemi için kullanılan rabbitmq'nun event ve commandlarının aynı namespace altında olması gerektiğinden Messages klasörü altında bu eventler oluşturuldu.

 -Services :
	-Counter Servisi: Database olarak Mongo DB kullanılmıştır. Bu api projesinde sayaç ekleme, sayaç güncelleme ve sayaç silme işlemlerinin yapılabileceği ortam hazırlanmıştır. Proje ayağa kalktığında database hazır dummy sayaçlar oluşturulması için scope içinde dummy sayaçlar eklendi.
	-Report Servisi:  Database olarak Mongo DB kullanılmıştır. Rapor durumundaki değişikliğin sağlanması amacıyla Consumer klasörü oluşturuldu. Bu projede sayaç numaralarına göre sıralama yapılarak rapor çıktısının excel olarak alınması sağlandı. Rapor oluştur dendiğinde rapor tablosuna rapor durumu 0 (hazırlanıyor) olan kayıt oluşturulduktan sonra mesaj kuyruk sistemi ile durumu 1(Tamamlandı) set ediliyor.

 -Gateways : Gelen isteklerin mikroservislerin port numarasıyla ilgilenilmeden tek bir port üzerinden haberleşmesi amacıyla bu proje oluşturuldu. Ocelot kütüphanesi kullanıldı ve tabii ki identity server ile koruma altına alındı.

  -Web : Web projesi mvc olarak hazırlandı ve sayfalar için boostrapt template kullanıldı.


  Projede yer alan tüm databaseler dockerize edildi ve proje ayağa kaldırılmadan önce aşağıda bahsedilen şekilde docker dosyalarının ayağa kaldırılması gerekmektedir.

  Adım 1: proje klasörü içinde MicroserviceDatabases adındaki klasörünün uzantısına powershell içinde gittikten sonra 
  (örn:
  cd C:\Users\Cansu\source\Repos\AkilliSayac\AkilliSayacVeriIslemeRaporlama\MicroserviceDatabases)

  docker-compose up

  komutunu çalıştırmanız gerekmektedir. Eğer docker desktop kurulu ise rabbitmqcontainer,identitydb,AkilliSayacDb container'larını görebilirsiniz. Container'lar ayağa kalktıktan sonra localinizde  Shared projesi hariç diğer tüm projeleri multiple seçmeniz ve çalıştırmanız gerekmektedir. Ardından tarayıcınızdan http://localhost:5010/ adresine gidebilirsiniz. Tüm port bilgileri Port.txt dosyasında yer almaktadır.

  Teşekkürler,

  Cansu
