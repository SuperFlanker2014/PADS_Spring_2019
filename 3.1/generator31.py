import random

random.seed(5)

with open('input.txt','w') as OUTPUT:
    OUTPUT.write('6000 6000\n')
    l = []
    for i in range(6000):
        n = random.randrange(1, 40001)
        l.append(str(n))
    OUTPUT.write(' '.join(l) + '\n')
    l = []
    for i in range(6000):
        n = random.randrange(1, 40001)
        l.append(str(n))
    OUTPUT.write(' '.join(l))