# -*- coding: utf-8 -*-
import numpy as np 
import pandas as pd
from scipy import signal
import matplotlib.pyplot as plt
import statistics 

from scipy.spatial.transform import Rotation as R

import math


df = pd.read_csv('datalev.txt', header = None, sep=';', dtype = float)

n = len(df)
print('кол-во отсчетов',n)
v1 = []
eye_vel=[]

tim = []

# a - начало диапазона b- его конец
a=8150
b=8210
for i in range(n-1):
    tim.append((df[0][i+1]-df[0][i])/ 10**7)

#фильтр

eye_filt = signal.medfilt(df[1], 5)
#v4 = signal.medfilt(eye_vel, 3)
lenE=len(eye_filt)

#расчет ск глаза
for i in range(a,b):
    tmp = (eye_filt[i+1] - eye_filt[i-1]) /(2* tim[i])
    v1.append(tmp)

vG=signal.medfilt(v1,7)

ot=3
q1=signal.medfilt(df[3], ot)
q2=signal.medfilt(df[4], ot)
q3=signal.medfilt(df[5], ot)
q4=signal.medfilt(df[6], ot)

#расчет ск головы
r = []
ar = []
for i in range(a,b):
    #r.append(R.from_quat([df[3][i], df[4][i], df[5][i], df[6][i]]))  
    r.append(R.from_quat([q1[i], q2[i], q3[i], q4[i]]))  
    #ar.append(r[i].as_euler('xyz', degrees=True))  

angLen=len(r)
for i in range(angLen):
    ar.append(r[i].as_euler('xyz', degrees=True)) 
ar = np.array(ar)

vHead = []
for i in range(len(ar)-1):
    vHead.append((ar[i+1,1]-ar[i,1]) / tim[i])

plt.plot(v1,label = "скорость по трем точкам")
# plt.plot(v3,label = "медианный фильтр")
plt.xlabel("Угловая скорость глаза")
plt.legend()
plt.show()

vH2=signal.medfilt(vHead, 7)
plt.plot(vH2,label = " фильтр скорости головы ")
#plt.plot(vHead,label = "скорость головы")
plt.ylabel("скорость поворота головы")
plt.legend()
plt.show()


    
v = []
vsum=[]
t = []
z=0
for i in range(angLen-1):
    mod = (vH2[i] + vG[i])
    vsum.append(mod)
    if mod <= 4:
        v.append(mod)
        t.append((df[0][i+1]-df[0][i])/10**7)

sumt = sum(t)
sumT = (df[0][b] - df[0][a])/10**7

plt.plot(vsum,label = "скорость суммарная")
plt.legend()
plt.show()
#fig, axs = plt.subplots(1, 1, tight_layout=True, figsize=(10,10))
#axs[0].plot(vH2,label = " фильтр скорости головы ")
#axs[0].set_title('FCI голова ')
#axs[0].plot(v1,label = "скорость глаза ")
#axs[0].set_title('-глаз- ')
#axs[0].plot(vsum, label = "суммарная ")
#axs[0].set_title(" скорость ")
fig= plt.figure()
ax=fig.add_axes([0,0,1,1])
ax.plot(vH2, label="скорость головы")
ax.plot(vsum, label="скорость суммарная")
ax.plot(vG, label="скорость глаза ")

ax.legend();



print("время четкого видения",sumt, 'общее время',sumT)

print('коэф четкого видения',sumt/sumT)

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
