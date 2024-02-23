using Xunit;

namespace LinkedList
{
    public class DoublyCircularLinkedListFacts
    {
        [Fact]
        public void Add_ListIsEmpty_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<int>() { 2 };

            Assert.Equal(2, list.First!.Value);
            Assert.Equal(2, list.Last!.Value);
            Assert.Single(list);
        }

        [Fact]
        public void Add_ThreeNodesAreAdded_ShouldReturnExpectdResult()
        {
            int[] numbers = { 1, 2, 3 };

            var list = new DoublyCircularLinkedList<int>(numbers);

            Assert.Equal(1, list.First!.Value);
            Assert.Equal(2, list.First!.Next!.Value);
            Assert.Equal(3, list.Last!.Value);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void AddFirst_AddReceiveValue_ShouldReturnExpectedResult()
        {
            var charList = new DoublyCircularLinkedList<char>();
            var b = charList.AddFirst('b');
            var a = charList.AddFirst('a');

            Assert.Equal(a, charList.First);
            Assert.Equal(b, charList.First!.Next);
            Assert.Equal(b, a.Next);
            Assert.Equal(a, b.Previous);
            Assert.Equal(2, charList.Count);
        }

        [Fact]
        public void AddFirst_ListIsEmpty_ShouldReturnExpectedResult()
        {
            var nations = new DoublyCircularLinkedList<string>();
            nations.AddFirst(new Node<string>("Italy"));

            Assert.Equal("Italy", nations.First!.Value);
            Assert.Equal("Italy", nations.Last!.Value);
            Assert.Single(nations);
        }

        [Fact]
        public void AddLast_ListContainsMoreNodes_ShouldReturnExpectedResult()
        {
            int[] numbers = { 1, 2, 3 };
            var numbersList = new DoublyCircularLinkedList<int>(numbers);
            numbersList.AddLast(4);

            Assert.Equal(1, numbersList!.First!.Value);
            Assert.Equal(2, numbersList!.First.Next!.Value);
            Assert.Equal(3, numbersList!.First!.Next!.Next!.Value);
            Assert.Equal(4, numbersList!.Last!.Value);
            Assert.Equal(4, numbersList.Count);

            Assert.Equal(1, numbersList.Last.Next!.Next!.Value);
            Assert.Equal(4, numbersList.First.Previous!.Previous!.Value);
        }

        [Fact]
        public void AddAfter_NodesAreAddedAfterFirstAndLastNode_ShouldReturnExpectedResult()
        {
            var roma = new Node<string>("Roma");
            var inter = new Node<string>("Inter");
            var milan = new Node<string>("Milan");

            var teams = new DoublyCircularLinkedList<string>();
            teams.AddFirst(roma);
            teams.AddAfter(roma, inter);
            teams.AddAfter(inter, milan);

            Assert.Equal(roma, teams.First);
            Assert.Equal(inter, teams.First!.Next);
            Assert.Equal(milan, teams.First.Next!.Next);

            Assert.Equal(roma, milan.Next!.Next);
            Assert.Equal(milan, roma.Previous!.Previous);
        }

        [Fact]
        public void AddAfter_AddAfterSecondNode_ShouldReturnExpectedResult()
        {
            char[] vowels = { 'a', 'i', 'o', 'u' };
            var vowelsList = new DoublyCircularLinkedList<char>(vowels);
            Node<char> e = new('e');

            vowelsList.AddAfter(vowelsList.First!.Next!, e);

            Assert.Equal(e, vowelsList.First.Next!.Next);
            Assert.Equal('i', e.Previous!.Value);
            Assert.Equal('o', e.Next!.Value);
            Assert.Equal(5, vowelsList.Count);
        }

        [Fact]
        public void AddAfter_NodeIsNull_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<int>();

            Assert.Null(list.First);
            Assert.Throws<ArgumentNullException>(() => list.AddAfter(list.First!, null));
        }

        [Fact]
        public void AddAfter_NodeIsNotOnTheCurrentListAndNewNodeBelongsToAnotherList_ShouldReturnExpectedResult()
        {
            Node<int> eleven = new(11);

            var firstList = new DoublyCircularLinkedList<int>() { 1, 2, 3, 4, 5 };
            var secondList = new DoublyCircularLinkedList<int>() { 6, 7, 8, 9, 10 };

            Assert.Throws<InvalidOperationException>(() => firstList.AddAfter(eleven, new Node<int>(44)));
            Assert.Throws<InvalidOperationException>(() => firstList.AddAfter(secondList.First!, firstList.First!));
        }

        [Fact]
        public void AddBefore_NodeAndNewNodeAreNull_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<char>() { 'x', 'y', 'z' };

            Assert.Throws<ArgumentNullException>(() => list.AddBefore(null, new Node<char>('q')));
            Assert.Throws<ArgumentNullException>(() => list.AddBefore(list.Last!, null));
        }

        [Fact]
        public void AddBefore_NodeIsNotOnTheCurrentListAndNewNodeBelongsToAnotherList_ShouldReturnExpectedResult()
        {
            Node<string> b = new("b");

            var firstList = new DoublyCircularLinkedList<string>() { "q", "w", "e", "r", "t", "y" };
            var secondList = new DoublyCircularLinkedList<string>() { "c", "d", "k", "r", };

            Assert.Throws<InvalidOperationException>(() => firstList.AddBefore(b, new Node<string>("z")));
            Assert.Throws<InvalidOperationException>(() => firstList.AddBefore(secondList.First!, firstList.First!));
        }

        [Fact]
        public void AddBefore_NodeIsAddedBeforeFirst_ShouldReturnExpectedResult()
        {
            int[] digits = { 1, 2, 3, 4 };
            var numbersList = new DoublyCircularLinkedList<int>(digits);
            var zero = new Node<int>(0);

            numbersList.AddBefore(numbersList.First!, zero);

            Assert.Equal(zero, numbersList.First);
            Assert.Equal(1, zero.Next!.Value);
            Assert.Equal(zero.Previous!.Previous, numbersList.Last);
        }

        [Fact]
        public void AddBefore_NodeIsAddedBeforeLast_ShouldReturnExpectedResult()
        {
            string[] cars = { "bmw", "mercedes", "audi" };
            var carsList = new DoublyCircularLinkedList<string>(cars);
            var porsche = new Node<string>("porsche");

            carsList.AddBefore(carsList.Last!, porsche);

            Assert.Equal(porsche.Next, carsList.Last);
            Assert.Equal(porsche, carsList.Last!.Previous);
            Assert.Equal("mercedes", porsche.Previous!.Value);
            Assert.Equal(4, carsList.Count);
        }

        [Fact]
        public void First_ListIsEmpty_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<int>();

            Assert.Null(list.First);
            Assert.Null(list.Last);
        }

        [Fact]
        public void Clear_AllNodesAreRemoved_ShouldReturnExpectedResult()
        {
            string[] daysOfTheWeek = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
            var daysList = new DoublyCircularLinkedList<string>(daysOfTheWeek);
            daysList.Clear();

            Assert.Null(daysList.First);
            Assert.Null(daysList.Last);
            Assert.Empty(daysList);
        }

        [Fact]
        public void Clear_ListIsEmpty_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<double>();
            list.Clear();

            Assert.Null(list.First);
            Assert.Null(list.Last);
            Assert.Empty(list);
        }

        [Fact]
        public void Contains_ListContainsTargetValue_ShouldReturnExpectedResult()
        {
            int[] numbers = { 2, 4, 6, 8 };
            var numbersList = new DoublyCircularLinkedList<int>(numbers);
            bool result = numbersList.Contains(8);

            Assert.True(result);
        }

        [Fact]
        public void Contains_ListDoNotContainTargetValue_ShouldReturnExpectedResult()
        {
            int[] numbers = { 2, 4, 6, 8 };
            var numbersList = new DoublyCircularLinkedList<int>(numbers);
            bool result = numbersList.Contains(15);

            Assert.False(result);
        }

        [Fact]
        public void CopyTo_ArrayIsNull_ShouldReturnExpectedResult()
        {
            var charList = new DoublyCircularLinkedList<char> { 'a', 'b', 'c' };

            Assert.Throws<ArgumentNullException>(() => charList.CopyTo(null, 0));
        }

        [Fact]
        public void CopyTo_ArrayIndexIsLessThanZero_ShouldReturnExpectedResult()
        {
            var namesList = new DoublyCircularLinkedList<string> { "Chris", "Marcus", "Francesco" };
            string[] array = new string[3];

            Assert.Throws<ArgumentOutOfRangeException>(() => namesList.CopyTo(array, -1));
        }

        [Fact]
        public void CopyTo_ArrayDoesNotHaveEnoughSpace_ShouldReturnExpectedResult()
        {
            var numbersList = new DoublyCircularLinkedList<int> { 7, 12, 44, 65, 77, 88, 100 };
            var array = new int[6];

            Assert.Throws<ArgumentException>(() => numbersList.CopyTo(array, 0));
        }

        [Fact]
        public void CopyTo_EntireListIsCopied_ShouldReturnExpectedResult()
        {
            var numbersList = new DoublyCircularLinkedList<int> { 1, 2, 3, 4, 5 };
            int[] array = new int[5];
            numbersList.CopyTo(array, 0);

            Assert.Equal(numbersList.First!.Value, array[0]);
            Assert.Equal(numbersList.First.Next!.Value, array[1]);
            Assert.Equal(numbersList.First.Next.Next!.Value, array[2]);
            Assert.Equal(numbersList.First.Next.Next!.Next!.Value, array[3]);
            Assert.Equal(numbersList.First.Next.Next!.Next!.Next!.Value, array[4]);
        }

        [Fact]
        public void Find_TargetNodeIsFound_ShouldReturnExpectedResult()
        {
            int[] numbers = { 2, 4, 7, 11 };
            var numbersList = new DoublyCircularLinkedList<int>(numbers);

            Assert.Equal(numbersList.Last!.Previous, numbersList.Find(7));
        }

        [Fact]
        public void Find_TargetNodeIsNotFound_ShouldReturnExpectedResult()
        {
            char[] vowels = { 'A', 'E', 'I', 'O' };
            var charList = new DoublyCircularLinkedList<char>(vowels);

            Assert.Null(charList.Find('U'));
        }

        [Fact]
        public void FindLast_TargetNodeIsFound_ShouldReturnExpectedResult()
        {
            char[] consons = { 'z', 'g', 'z', 'f', 'd' };
            var consonsList = new DoublyCircularLinkedList<char>(consons);
            var targetNode = consonsList.FindLast('z');

            Assert.Equal('f', targetNode!.Next!.Value);
            Assert.Equal('g', targetNode!.Previous!.Value);
        }

        [Fact]
        public void GetEnumerator_ListOfIntegers_ShouldReturnExpectedResult()
        {
            var intList = new DoublyCircularLinkedList<int> { 10, 20, 30, 40};
            IEnumerator<int> enumerator = intList.GetEnumerator();
            int sum = 0;

            while(enumerator.MoveNext())
            {
                sum += enumerator.Current;
            }

            Assert.Equal(100, sum);
        }

        [Fact]
        public void Remove_SecondNodeIsRemoved_ShouldReturnExpectedResult()
        {
            string[] names = { "John", "Michael", "Harry" };
            var namesList = new DoublyCircularLinkedList<string>(names);
            namesList.Remove(namesList.First!.Next!);

            Assert.Equal("Harry", namesList.First!.Next!.Value);
            Assert.Equal("John", namesList.First!.Next!.Previous!.Value);
            Assert.Equal("John", namesList.First!.Next!.Next!.Next!.Value);
            Assert.Equal("Harry", namesList.First!.Previous!.Previous!.Value);
            Assert.Equal(2, namesList.Count);
        }

        [Fact]
        public void Remove_NodeIsNull_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<int>();

            Assert.Throws<ArgumentNullException>(() => list.Remove(null));
        }

        [Fact]
        public void Remove_RemoveReceiveValue_ShouldReturnExpectedResult()
        {
            string[] food = { "pasta", "french fries", "rice", "pizza" };
            var foodList = new DoublyCircularLinkedList<string>(food);

            Assert.True(foodList.Remove("rice"));
            Assert.Equal(3, foodList.Count);
        }

        [Fact]
        public void Remove_ListIsEmpty_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<int>();
            Assert.False(list.Remove(14));
        }

        [Fact]
        public void Remove_FirstNodeIsRemovedAndListIEmpty_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<int>();

            Assert.Throws<ArgumentNullException>(() => list.Remove(list.First));
        }

        [Fact]
        public void Remove_NodeDoesNotBelongToTheList_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<int> { 2, 4, 6 };
            Assert.Throws<InvalidOperationException>(() => list.Remove(new Node<int>(8)));
        }

        [Fact]
        public void RemoveFirst_FirstNodeIsRemoved_ShouldReturnExpectedResult()
        {
            int[] numbers = { 10, 20, 30 };
            var numbersList = new DoublyCircularLinkedList<int>(numbers);
            numbersList.RemoveFirst();

            Assert.Equal(20, numbersList.First!.Value);
            Assert.Equal(2, numbersList.Count);
        }

        [Fact]
        public void RemoveLast_LastNodeIsRemoved_ShouldReturnExpectedResult()
        {
            char[] chars = { 'q', 'a', 'b', 'c', 'e' };
            var charsList = new DoublyCircularLinkedList<char>(chars);
            charsList.RemoveLast();

            Assert.Equal('c', charsList.Last!.Value);
            Assert.Equal('b', charsList.Last!.Previous!.Value);
            Assert.Equal('q', charsList.Last!.Next!.Next!.Value);
            Assert.Equal(4, charsList.Count);
        }

        [Fact]
        public void RemoveLast_RemoveFirstAndRemoveLastAreCalledOnEmptyList_ShouldReturnExpectedResult()
        {
            var list = new DoublyCircularLinkedList<string>();

            Assert.Throws<InvalidOperationException>(list.RemoveFirst);
            Assert.Throws<InvalidOperationException>(list.RemoveLast);
        }
    }
}