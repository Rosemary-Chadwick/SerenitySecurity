import React, { useEffect, useRef } from 'react';

const DarkMatrixBackground = () => {
  const canvasRef = useRef(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    const context = canvas.getContext('2d');
    
    const resize = () => {
      canvas.width = window.innerWidth;
      canvas.height = window.innerHeight;
    };
    
    window.addEventListener('resize', resize);
    resize();

    const chars = '01278+-*/=(){}[]<>!&|∑∫πλΩ√∆'.split('');
    const fontSize = 12;
    const columns = Math.floor(canvas.width / fontSize);
    const rows = Math.floor(canvas.height / fontSize);
    
    const grid = Array(rows).fill().map(() => 
      Array(columns).fill().map(() => ({
        char: chars[Math.floor(Math.random() * chars.length)],
        opacity: Math.random() > 0.75 ? Math.random() * 0.05 : 0 
      }))
    );

    const draw = () => {
      context.fillStyle = 'rgba(0, 20, 40, 0.3)';
      context.fillRect(0, 0, canvas.width, canvas.height);

      context.font = fontSize + 'px monospace';

      for (let i = 0; i < rows; i++) {
        for (let j = 0; j < columns; j++) {
          const cell = grid[i][j];
          
          if (Math.random() < 0.01) {
            if (cell.opacity === 0 && Math.random() > 0.75) {
              cell.char = chars[Math.floor(Math.random() * chars.length)];
              cell.opacity = Math.random() * 0.15 + 0.05; // Reduced opacity range: 0.05 to 0.2
            } else if (cell.opacity > 0) {
              cell.opacity = 0;
            }
          }

          context.fillStyle = `rgba(255, 253, 208, ${cell.opacity * 2})`; // Increased opacity for better visibility
          context.fillText(cell.char, j * fontSize, (i + 1) * fontSize);
        }
      }
    };

    const interval = setInterval(draw, 150);

    return () => {
      clearInterval(interval);
      window.removeEventListener('resize', resize);
    };
  }, []);

  return (
    <canvas
      ref={canvasRef}
      style={{ 
        position: 'fixed',
        top: 0,
        left: 0,
        width: '100%',
        height: '100%',
        pointerEvents: 'none',
        zIndex: -1
      }}
    />
  );
};

export default DarkMatrixBackground;