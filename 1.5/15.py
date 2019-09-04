def merge(a,b):
    c = []
    while len(a) != 0 and len(b) != 0:
        if a[0] < b[0]:
            c.append(a[0])
            a.remove(a[0])
        else:
            c.append(b[0])
            b.remove(b[0])
    if len(a) == 0:
        c += b
    else:
        c += a
    return c

# def sort3(x,logger,m=0):
#     middle = len(x) // 2
#     if len(x) == 0 or len(x) == 1:
#         return x
#     else:
#         a = sort3(x[:middle],logger,m)
#         b = sort3(x[middle:],logger,m+middle)
#     return merge(a,b,m+middle)

def sort4(A, logger):
    i = 0
    while i < len(A):
        smallest = min(A[i:])
        index_of_smallest = A[i:].index(smallest) + i
        if i != index_of_smallest:
            A[i], A[index_of_smallest] = A[index_of_smallest], A[i]
            logger(i, index_of_smallest)
        i = i + 1
    return A

def sort3(A, logger):
    done = False
    while not done:
        done = True
        for i in range(len(A) - 1):
            if A[i] > A[i + 1]:
                A[i], A[i + 1] = A[i + 1], A[i]
                logger(i, i+1)
                done = False
    return A

def sort2(A, logger):
    middle = len(A) // 2
    a = A[:middle]
    b = A[middle:]

    for i in range(1, len(a)):
        j = i
        while (j > 0) & (a[j - 1] > a[j]):
            a[j-1], a[j] = a[j], a[j-1]
            logger(j-1, j)
            j = j-1
    for i in range(1, len(b)):
        j = i
        while (j > 0) & (b[j - 1] > b[j]):
            b[j-1], b[j] = b[j], b[j-1]
            logger(middle+j-1, middle+j)
            j = j-1
    c = merge(a,b)
    # for i in range(1, len(c)):
    #     j = i
    #     while (j > 0) & (c[j - 1] > c[j]):
    #         c[j-1], c[j] = c[j], c[j-1]
    #         logger(j, j+1)
    #         j = j-1
    return c

def sort1(A, logger):
    for i in range(1, len(A)):
        j = i
        while (j > 0) & (A[j - 1] > A[j]):
            A[j-1], A[j] = A[j], A[j-1]
            logger(j-1, j)
            j = j-1
    return A

import time
start = time.time()
with open('input.txt','r') as INPUT:
    lines = INPUT.readlines()
    n = int(lines[0])
    original_list = [int(i) for i in lines[1].split(' ')]

    logger = lambda ind1, ind2: OUTPUT.write(f'Swap elements at indices {str(ind1+1)} and {str(ind2+1)}.\n')

    with open('output.txt', 'w') as OUTPUT:
        A = sort4(original_list, logger)
        OUTPUT.write('No more swaps needed.\n')
        OUTPUT.write(' '.join([str(i) for i in A]))
end = time.time()

elapsed = end - start
print(elapsed)