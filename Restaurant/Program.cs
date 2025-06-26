using System;

namespace Restaurant
{
    public static class Program
    {
        private static Menu menu = new();
        private static Cart cart = new();

        private static void Main()
        {
            // 초기 메뉴 등록
            menu.AddItem(new MenuItem("스테이크", 15000));
            menu.AddItem(new MenuItem("파스타", 12000));
            menu.AddItem(new MenuItem("피자", 9000));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("메인 메뉴");
                menu.DisplayMenu();

                Console.WriteLine("\n주문하실 메뉴 번호를 입력해주세요.");
                Console.WriteLine("0: 종료 | 100: 장바구니 이동 | 200: 결제하기");

                string? input = Console.ReadLine();

                if (input == "0")
                    break;

                if (input == "100")
                {
                    CartConsole();
                    continue;
                }

                if (input == "200")
                {
                    Console.WriteLine("\n결제는 아직 준비 중입니다.");
                    Console.WriteLine("아무 키나 누르면 메인 메뉴로 돌아갑니다.");
                    Console.ReadKey();
                    continue;
                }

                if (!int.TryParse(input, out int index))
                {
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 누르면 계속...");
                    Console.ReadKey();
                    continue;
                }

                var item = menu.GetItemByIndex(index - 1);
                if (item == null)
                {
                    Console.WriteLine("존재하지 않는 메뉴입니다. 아무 키나 누르면 계속...");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine($"{item.Name}의 수량을 입력하세요:");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("잘못된 수량입니다. 아무 키나 누르면 계속...");
                    Console.ReadKey();
                    continue;
                }

                cart.AddItem(item, quantity);
                Console.WriteLine("✅ 장바구니에 추가되었습니다!");
                Console.WriteLine("\n아무 키나 누르면 계속...");
                Console.ReadKey();
            }

            Console.WriteLine("프로그램을 종료합니다.");
        }

        private static void CartConsole()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("현재 장바구니:");
                cart.DisplayCart();

                Console.WriteLine("\n1. 항목 삭제");
                Console.WriteLine("2. 수량 변경");
                Console.WriteLine("0. 메인 메뉴로 돌아가기");
                Console.Write("작업을 선택하세요: ");
                string? input = Console.ReadLine();

                if (input == "0") break;

                switch (input)
                {
                    case "1":
                        Console.Write("삭제할 메뉴 이름을 입력하세요: ");
                        string? nameToRemove = Console.ReadLine();
                        cart.RemoveItem(nameToRemove ?? "");
                        Console.WriteLine("✅ 삭제 완료. 아무 키나 누르면 계속...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("수량을 변경할 메뉴 이름을 입력하세요: ");
                        string? nameToChange = Console.ReadLine();
                        Console.Write("새 수량을 입력하세요: ");
                        if (int.TryParse(Console.ReadLine(), out int newQty) && newQty > 0)
                        {
                            cart.ChangeQuantity(nameToChange ?? "", newQty);
                            Console.WriteLine("✅ 수량 변경 완료. 아무 키나 누르면 계속...");
                        }
                        else
                        {
                            Console.WriteLine("잘못된 수량입니다.");
                        }
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("잘못된 선택입니다. 아무 키나 누르면 계속...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
