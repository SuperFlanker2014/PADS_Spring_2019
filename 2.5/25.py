def Quicksort(A, left, right):
    i = left
    j = right
    key = A[(left + right) // 2]
    while i - j <= 0:
        while A[i] < key:
            i = i + 1
        while A[j] > key:
            j = j - 1
        if i - j <= 0:
            A[i], A[j] = A[j], A[i]
            i = i + 1
            j = j - 1
    if (left < j):
        Quicksort(A, left, j)
    if (i < right):
        Quicksort(A, i, right)

def ver(m, A, a):
    if m == 1:
        return "YES"
    for i in range(len(a)):
        k = 0
        j = 0
        while j < len(A[a[i][0]]):
            if abs(i - A[a[i][0]][j]) % m == 0:
                k += 1;
                A[a[i][0]].pop(j)
            j += 1;
        if (k == 0):
            return "NO"
    return "YES"

with open('input.txt','r') as INPUT:
    lines = INPUT.readlines()
    n, k = [int(i) for i in lines[0].split()]
    numbers = [int(i) for i in lines[1].split()]

    numbersNew = numbers[:]

    dictBeforeSort = dict()
    for i in range(n):
        # numbersNew[i] = [numbersNew[i], i]
        dictBeforeSort[numbersNew[i]] = dictBeforeSort.get(numbersNew[i], [])
        dictBeforeSort[numbersNew[i]].append(i)

    print(numbers, dictBeforeSort)

    Quicksort(numbersNew, 0 , len(numbersNew) - 1)

    dictAfterSort = dict()
    for i in range(n):
        dictAfterSort[numbersNew[i]] = dictAfterSort.get(numbersNew[i], [])
        dictAfterSort[numbersNew[i]].append(i)

    flag = True

    for key in dictAfterSort.keys():
        if len(dictAfterSort[key]) == 1:
            if abs(dictAfterSort[key][0] - dictBeforeSort[key][0]) % k != 0:
                flag = False
                break
        else:
            break

    print(n, k)
    print(numbersNew, dictAfterSort)

    # with open('output.txt', 'w') as OUTPUT:
    #     OUTPUT.write(result)