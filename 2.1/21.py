def merge(a, b):
    i, j = 0, 0
    n, m = len(a), len(b)
    c = []
    while i < n or j < m:
        if j == m or (i < n and a[i] <= b[j]):
            c.append(a[i])
            i = i + 1
        else:
            c.append(b[j])
            j = j + 1
    return c

def merge_sort(a, ind, logger):
    n = len(a)
    if n == 1:
        return a
    l = a[0:n//2]
    r = a[n//2:n]
    l = merge_sort(l,ind, logger)
    r = merge_sort(r,ind+n//2, logger)
    m = merge(l, r)
    logger(ind, ind + n - 1, m[0], m[-1])
    return m

with open('input.txt','r') as INPUT:
    lines = INPUT.readlines()
    n = int(lines[0])
    original_list = [int(i) for i in lines[1].split(' ')]
    logger = lambda i1,i2,i3,i4: OUTPUT.write(f'{i1+1} {i2+1} {i3} {i4}\n')

    with open('output.txt', 'w') as OUTPUT:
        A = merge_sort(original_list, 0, logger)
        OUTPUT.write(' '.join([str(i) for i in A]))