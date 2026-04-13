import { $, apiFetch, renderStatus, clearChildren, getQueryParam, setupPagination, setupPageSizeSelect } from '/scripts/common.js';

(async function initUsersIndex() {
	const page = Math.max(1, Number(getQueryParam('page') || localStorage.getItem('users_page') || '1'));
	const size = Math.min(100, Math.max(1, Number(getQueryParam('size') || localStorage.getItem('users_size') || '9')));
	localStorage.setItem('users_page', page);
	localStorage.setItem('users_size', size);
	const listEl = $('#user-list');
	const statusEl = $('#status');
	const tpl = $('#user-card');
	try {
		const payload = await apiFetch(`/users?page=${page}&size=${size}`);
		const items = Array.isArray(payload) ? payload : (payload.data || []);
		clearChildren(listEl);
		if (items.length === 0) {
			renderStatus(statusEl, 'warn', 'No users found for this page.');
		} else {
			renderStatus(statusEl, '', '');
			for (const u of items) {
				const frag = tpl.content.cloneNode(true);
				const root = frag.querySelector('.card');
				root.querySelector('.name').textContent = u.name ?? '—';
				root.querySelector('.email').textContent = u.email ?? '';
				root.querySelector('.btn-view').href = `/users/view.html?id=${encodeURIComponent(u.id)}`;
				root.querySelector('.btn-edit').href = `/users/edit.html?id=${encodeURIComponent(u.id)}`;
				root.querySelector('.btn-delete').dataset.id = u.id;
				listEl.appendChild(frag);
			}
		}
		listEl.addEventListener('click', async (ev) => {
			const btn = ev.target.closest('button.btn-delete[data-id]');
			if (!btn) return;
			const id = btn.dataset.id;
			if (!confirm('Delete this user? This cannot be undone.')) return;
			try {
				await apiFetch(`/users/${encodeURIComponent(id)}`, { method: 'DELETE' });
				renderStatus(statusEl, 'ok', `User ${id} deleted.`);
				setTimeout(() => location.reload(), 2000);
			} catch (err) {
				renderStatus(statusEl, 'err', `Delete failed: ${err.message}`);
			}
		});
		setupPageSizeSelect(size);
		setupPagination(payload, page, size);
	} catch (err) {
		renderStatus(statusEl, 'err', `Failed to fetch users: ${err.message}`);
	}
})();
