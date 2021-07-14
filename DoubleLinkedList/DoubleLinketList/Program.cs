using System;
using System.Text;

namespace DoubleLinketList
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDoubleLinkedList list = new MyDoubleLinkedList();
            list.Insert("1");
            list.Insert("2");
            list.Insert("3");

            DoubleLink link4 = list.Insert("4");
            list.Insert("5");
            Console.WriteLine("List: " + list);

            list.InsertAfter(link4, "[4a]");
            Console.WriteLine("List: " + list);
            Console.Read();

        }
    }

    public class DoubleLink
    {
        // Kullanacağımız ve liste içerisinde ileri-geri şeklinde değer okuyabileceğimiz yapıları denklare ettik.
        public string Title { get; set; }

        // Önceki link için sınıfı özellik olarak tanımladık.
        public DoubleLink PreviousLink { get; set; }

        // Sonra ki link için sınıfı özellik olarak tanımladık.
        public DoubleLink NextLink { get; set; }

        // bu sınıf newlenirken yani bir yerden çağırılırken bana title vermek zorundasın dedik. (Constructor)
        public DoubleLink(string title)
        {
            Title = title;
        }

        // Burada ToString metodunu ezerek Title'ı geri dönüyoruz.
        public override string ToString()
        {
            return Title;
        }
    }
    // Tüm listeleme işlemleri burada yapılmaktadır.
    public class MyDoubleLinkedList
    {
        // DoubleLink objesini baz alarak İlk değer şeklinde bir tanımlama yaptık.
        private DoubleLink _first;

        // Boş kontrolü yapabiliz. Eğer ilk değer boş ise geriye döner.
        public bool IsEmpty
        {
            get
            {
                return _first == null;
            }
        }

        // Bu sınıf ayağa kalkarken first objesine null setliyoruz.
        public MyDoubleLinkedList()
        {
            _first = null;
        }

        // Double linklere ekleme yaptığımız alan. Bizden bir title parametresi bekliyor.
        public DoubleLink Insert(string title)
        {
            // Bir bağlantı oluşturur ve bunu doublelinke insert eder.
            DoubleLink link = new DoubleLink(title);

            // Gelen değer listenin ilk öğesi olarak belirlenir.
            link.NextLink = _first;

            // Baştaki değer boş değilse bir öncesine set edilir.
            if (_first != null)
                _first.PreviousLink = link;
            _first = link;
            return link;
        }

        // Silme işlemleri yaptığımız alan. Bir parametre gerektirmiyor.
        public DoubleLink Delete()
        {
            // İlk öğeyi alır ve bağlı olduğu öğe olarak ayarlar
            DoubleLink temp = _first;
            if (_first != null)
            {
                _first = _first.NextLink;
                if (_first != null)
                    _first.PreviousLink = null;
            }
            return temp;
        }

        // String işlemleri burada yapılıyor.
        public override string ToString()
        {
            // Mevcut linki alıyoruz.
            DoubleLink currentLink = _first;

            // String builder tanımladık. Bazı string işlemleri yapacağız.
            StringBuilder builder = new StringBuilder();

            // mevcut link boş olmayana kadar..
            while (currentLink != null)
            {
                // builder içine mevcut linki ekledik.
                builder.Append(currentLink);
                currentLink = currentLink.NextLink;
            }
            return builder.ToString();
        }

        public void InsertAfter(DoubleLink link, string title)
        {
            // Gelen parametrelerden link boş ise veya gelen title boş ise boş değer döner.
            if (link == null || string.IsNullOrEmpty(title))
                return;
            // Gelen değerler standarta uygunsa..
            // Yeni bir link oluşturulur.
            DoubleLink newLink = new DoubleLink(title);
            newLink.PreviousLink = link;
            // 'after' bağlantısının bir sonraki referansını güncellenir, böylece önceki referansı yenisine işaret eder
            if (link.NextLink != null)
                link.NextLink.PreviousLink = newLink;
            // Düğümün sonraki bağlantısı alınır ve yeni bağlantımıza bağlanacak şekilde ayarlanır.
            newLink.NextLink = link.NextLink;
            link.NextLink = newLink;
        }
    }
}
