with open('input.txt','r') as INPUT:
    lines = INPUT.readlines()
    n = int(lines[0])

    result = []

    if n == 1:
        result = [1]
    elif n == 2:
        result = [1, 2]
    else:
        l = [1, 2]
        for i in range(3, n + 1):
            l.append(i)
            l[i // 2 if i % 2 == 1 else i // 2 - 1], l[-1] = l[-1], l[i // 2 if i % 2 == 1 else i // 2 - 1]
        result = l

    with open('output.txt', 'w') as OUTPUT:
        OUTPUT.write(' '.join([str(i) for i in result]))