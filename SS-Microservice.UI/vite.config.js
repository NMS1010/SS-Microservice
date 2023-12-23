import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import dns from 'dns'
import vitePluginRequire from 'vite-plugin-require';

dns.setDefaultResultOrder('verbatim')
// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react(), vitePluginRequire.default()],
    server: {
      watch: {
        usePolling: true,
      },
      host: true,
      strictPort: true,
      port: 5173,
    }
});
