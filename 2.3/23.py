cnt = 0

def Quicksort(A, left, right):
    global cnt
    i = left
    j = right
    key = A[(left + right) // 2]
    print(A, left, right, A[left:right+1])
    while i <= j:
        while A[i] < key:
            i = i + 1
            cnt = cnt + 1
        cnt = cnt + 1
        while A[j] > key:
            j = j - 1
            cnt = cnt + 1
        cnt = cnt + 1
        if (i <= j):
            A[i], A[j] = A[j], A[i]
            i = i + 1
            j = j - 1
    if (left < j):
        Quicksort(A, left, j)
    if (i < right):
        Quicksort(A, i, right)

def write_to_file(n):
    l = list(range(1, n + 1))

    result = []

    if n == 1:
        result = [1]
    elif n == 2:
        result = [1, 2]
    elif n == 3:
        result = [1, 3, 2]
    elif n == 4:
        result = [2, 4, 3, 1]
    elif n == 5:
        result = [2, 4, 5, 1, 3]
    elif n == 6:
        result = [2, 4, 6, 1, 3, 5]
    elif n == 7:
        result = [2, 4, 6, 7, 3, 5, 1]
    elif n == 8:
        result = [2, 4, 6, 8, 3, 5, 1, 7]
    else:
        result = [0] * n
        result[n // 2 if n % 2 == 1 else n // 2 - 1] = n
        for i in range(0, n // 2):
            result[i] = (i+1)*2

        lstRight = [i for i in range(1, n, 2)]

        v1 = result[:]
        indicesRightV1 = [i for i in range(n // 2 if n % 2 == 0 else n // 2 + 1, n, 2)] + [i for i in range(n // 2 + 1 if n % 2 == 0 else n // 2 + 2, n, 2)]
        for i in range(0, len(lstRight)):
            v1[indicesRightV1[i]] = lstRight[i]

        v2 = result[:]
        indicesRightV2 = [i for i in range(n // 2 + 1 if n % 2 == 0 else n // 2 + 2, n, 2)] + [i for i in range(n // 2 if n % 2 == 0 else n // 2 + 1, n, 2)]
        for i in range(0, len(lstRight)):
            v2[indicesRightV2[i]] = lstRight[i]

        global cnt

        cnt = 0
        Quicksort(v1[:], 0, n - 1)
        cntV1 = cnt;

        cnt = 0;
        Quicksort(v2[:], 0, n - 1)
        cntV2 = cnt;

        result = v1 if cntV1 > cntV2 else v2

        v3 = result[:]
        v3_i3 = v3.index(3)
        v3_i5 = v3.index(5)
        v3[v3_i3], v3[v3_i5] = v3[v3_i5], v3[v3_i3]

        v4 = result[:]
        v4_i5 = v4.index(5)
        v4_i7 = v4.index(7)
        v4[v4_i5], v4[v4_i7] = v4[v4_i7], v4[v4_i5]

        cnt = 0
        Quicksort(v3[:], 0, n - 1)
        cntV3 = cnt;

        cnt = 0;
        Quicksort(v4[:], 0, n - 1)
        cntV4 = cnt;

        if (max(cntV3, cntV4) > max(cntV1, cntV2)):
            result = v3 if cntV3 > cntV4 else v4

    with open('output.txt', 'w') as OUTPUT:
        OUTPUT.write(' '.join([str(i) for i in result]))

# with open('input.txt','r') as INPUT:
#     lines = INPUT.readlines()
#     n = int(lines[0])
#     write_to_file(n)

cnt = 0;
#[2, 4, 6, 8, 10, 12, 14, 15, 1, 9, 5, 11, 3, 13, 7]
Quicksort([1, 4, 6, 8, 10, 12, 14, 15, 2, 9, 5, 11, 3, 13, 7], 0, 14)
print(cnt)
#
# cnt = 0;
# #[2, 4, 6, 8, 10, 12, 14, 15, 1, 9, 5, 11, 3, 13, 7]
# Quicksort([2, 4, 6, 8, 10, 12, 14, 16, 17, 9, 1, 11, 3, 13, 5, 15, 7], 0, 16)
# print(cnt)
#
# cnt = 0;
# #[2, 4, 6, 8, 10, 12, 14, 15, 1, 9, 5, 11, 3, 13, 7]
# Quicksort([2, 4, 6, 8, 10, 12, 14, 16, 17, 19, 1, 11, 3, 13, 7, 15, 5, 17, 9], 0, 18)
# print(cnt)