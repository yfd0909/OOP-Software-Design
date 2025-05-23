### 객지 스터디 1주차 과제

<pre>
<code>
1.
<Animal 클래스> : _id, _name, _species 멤버 필드로 구성되어 생성자로 데이터를 저장하고 Equals 함수를 통해 오브젝트가 Animal 인지 확인한다. 
GetHashCode 함수를 public 으로 만들어서 id, name, species 값들을 정수값으로 만들어 반환하도록 하여 데이터를 비교하기 쉽도록 만든 것 같다.
<AnimalCollection 클래스> : 리스트를 만들어서 Animal 객체들을 저장하도록 만들었다. 추가하려는 Animal 객체가 이미 존재하면 false, 없다면 추가하고 true 반환하여 저장함. 
제거 함수도 있음. 각각 id, name, species 에 따라 데이터를 찾는 FindAnimalBy ~~ 함수들이 존재한다. FindAllAnimals 함수는 [.. 변수이름] 이런 식으로 쓰는 것을 처음 
봤는데 알아보니 C# 의 '컬렉션 식 기능' 을 이용해서 _animals 에 있는 항목들을 얕은 복사를 하여 새로운 컬렉션으로 만들어 반환하는 방식이다.
<Application 클래스> : 전반적으로 눈에 보이는 출력을 담당하는 클래스로 유저가 선택할 메뉴를 출력하고 P, A, Q 키를 입력 받음에 따라 Animal 출력, Animal 추가, 프로그램 
종료를 하는 방식.
<Program 클래스> : 어플리케이션을 시작.
<Species 클래스> : species 클래스 객체를 생성할 때 문자열이 잘못된 것을 방지하고 비교하는 등의 방식으로 제대로 된 불변성있는 개체 생성을 하게 함.

2.
AnimalCollection 에서 Animal 클래스로 생성된 개체들을 리스트에 저장한다.
Application 에서 AnimalCollection 클래스로 생성된 리스트를 이용하여 정보를 불러오거나
정보를 추가하는 등의 작업을 한다
Program 에서 어플리케이션을 실행하도록 한다.

3.
Animal 클래스에서 멤버 필드 _id, _name, _species 를 포함해서 다른 클래스의 
필드들이 private readonly 로 선언이 돼 있음. readonly 는 상수와 비슷한 개념이어서
보통 생성자에서 한 번 값이 들어가는 걸로 알고 있고 이를 보호하기 위해 각 변수에
프로퍼티를 사용해서 따로 접근을 해주었다.
Application 에서 TextReader 사용으로 읽기만 가능하도록 캡슐화가 되어있다.
using system.IO 를 해줘야 한다고 함.

4.
Animal 클래스의 멤버 필드에 더 넣고 싶은 요소를 추가하기 용이하고 (성별이나 나이같은 요소) 
AnimalCollection 에서 원하는 자료를 찾는 방식도 비슷한 메서드를 응용해서 쉽게 확장할 수 있을 거 같다.
</code>
</pre>
