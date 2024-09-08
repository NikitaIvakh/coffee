import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      'components': path.resolve(__dirname, './src/components'),
      'declarations': path.resolve(__dirname, './src/declarations'),
      'errors': path.resolve(__dirname, './src/errors'),
      'features': path.resolve(__dirname, './src/features'),
      'hooks': path.resolve(__dirname, './src/hooks'),
      'pages': path.resolve(__dirname, './src/pages'),
      'resources': path.resolve(__dirname, './src/resources'),
      'store': path.resolve(__dirname, './src/store'),
      'styles': path.resolve(__dirname, './src/styles'),
      'types': path.resolve(__dirname, './src/types'),
      'utils': path.resolve(__dirname, './src/utils'),
    },
  },
  server: {
    port: 3000,
  },
});
