import React, { useEffect } from 'react';

const MatrixDebug = () => {
  useEffect(() => {
    console.log('MatrixDebug component mounted');
    
    // Create canvas directly without React
    const canvas = document.createElement('canvas');
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    canvas.style.position = 'fixed';
    canvas.style.top = '0';
    canvas.style.left = '0';
    canvas.style.width = '100%';
    canvas.style.height = '100%';
    canvas.style.zIndex = '-1';
    canvas.style.pointerEvents = 'none';
    
    document.body.appendChild(canvas);
    
    const context = canvas.getContext('2d');
    
    // Fill with a very visible color for debugging
    context.fillStyle = 'rgba(0, 0, 255, 0.3)'; // Blue with 0.3 opacity
    context.fillRect(0, 0, canvas.width, canvas.height);
    
    console.log('Debug canvas created');
    
    // Clean up on unmount
    return () => {
      console.log('MatrixDebug component unmounting');
      document.body.removeChild(canvas);
    };
  }, []);
  
  return null; // This component doesn't render anything itself
};

export default MatrixDebug;