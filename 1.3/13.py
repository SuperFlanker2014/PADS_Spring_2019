with open('input.txt','r') as INPUT:
    lines = INPUT.readlines()
    n = int(lines[0])
    A = [int(i) for i in lines[1].split(' ')]
    B = [1]*len(A)

    first = A[0]
    for i in range(1, n):
        j = i
        originalItem = A[i]
        while (j > 0) & (A[j-1] > A[j]):
            A[j-1], A[j] = A[j], A[j-1]
            j = j-1
        B[i] = A.index(originalItem) + 1
        # if i == 1:
        #     B[0] = A.index(first) + 1

    strB = (' '.join([str(i) for i in B]))
    strA = (' '.join([str(i) for i in A]))

    with open('output.txt','w') as OUTPUT:
        OUTPUT.write(strB)
        OUTPUT.write('\n')
        OUTPUT.write(strA)