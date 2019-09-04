with open('input.txt','r') as INPUT:
    lines = INPUT.readlines()
    n = int(lines[0])
    original_list = [float(i) for i in lines[1].split(' ')]
    A = original_list[:]
    A.sort()

    # for i in range(1, n):
    #     j = i
    #     while (j > 0) & (A[j-1][1] > A[j][1]):
    #         A[j-1], A[j] = A[j], A[j-1]
    #         j = j-1

    first = original_list.index(A[0]) + 1
    middle = original_list.index(A[(len(A)-1)//2]) + 1
    last = original_list.index(A[-1]) + 1

    with open('output.txt','w') as OUTPUT:
        OUTPUT.write(str(first) + ' ' + str(middle) + ' ' + str(last))