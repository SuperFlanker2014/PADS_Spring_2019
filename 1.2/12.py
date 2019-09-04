with open('input.txt','r') as INPUT:
    a,b = [int(i) for i in INPUT.readlines()[0].split(' ')]
    with open('output.txt','w') as OUTPUT:
        OUTPUT.write(str(a+b**2))

